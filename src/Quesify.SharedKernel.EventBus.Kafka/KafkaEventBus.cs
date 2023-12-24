using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Quesify.EventBus.Events;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.EventBus.Events;
using System.Text.RegularExpressions;

namespace Quesify.SharedKernel.EventBus.Kafka;

public class KafkaEventBus : EventBusBase
{
    private readonly KafkaOptions _kafkaOptions;
    private readonly IProducer<Null, byte[]> _producer;
    private readonly IConsumer<Null, byte[]> _consumer;
    private readonly IAdminClient _adminClient;
    private readonly IKafkaSerilazer _kafkaSerilazer;
    private readonly ILogger<KafkaEventBus> _logger;

    public KafkaEventBus(
        IEventBusSubscriptionsManager eventBusSubscriptionsManager,
        IServiceProvider serviceProvider,
        EventBusOptions eventBusOptions,
        KafkaOptions kafkaOptions,
        IKafkaSerilazer kafkaSerilazer,
        ILogger<KafkaEventBus> logger)
        : base(eventBusSubscriptionsManager, serviceProvider, eventBusOptions)
    {
        _kafkaOptions = kafkaOptions;
        _producer = new ProducerBuilder<Null, byte[]>(_kafkaOptions.ProducerConfig).Build();
        _consumer = new ConsumerBuilder<Null, byte[]>(_kafkaOptions.ConsumerConfig).Build();
        _adminClient = new AdminClientBuilder(_kafkaOptions.AdminClientConfig).Build();
        _kafkaSerilazer = kafkaSerilazer;
        _logger = logger;
    }

    public override async Task PublishAsync(IntegrationEvent integrationEvent)
    {
        var eventName = integrationEvent.GetType().Name;
        var topicName = ProcessEventName(eventName);
        var body = _kafkaSerilazer.Serilaze(integrationEvent);
        var message = new Message<Null, byte[]> { Value = body };
        _logger.LogTrace("Publishing event to Kafka: {EventId}", integrationEvent.Id);
        await _producer.ProduceAsync(topicName, message);
    }

    public override async Task SubscribeAsync<T, TH>()
    {
        var eventName = typeof(T).Name;
        SubscriptionsManager.AddSubscription<T, TH>();
        _logger.LogTrace("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
        StartConsume(eventName);
        await Task.CompletedTask;
    }

    public override async Task UnsubscribeAsync<T, TH>()
    {
        var eventName = SubscriptionsManager.GetEventName<T>();
        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);
        SubscriptionsManager.RemoveSubscription<T, TH>();
        await Task.CompletedTask;
    }

    private void StartConsume(string eventName)
    {
        Task.Factory.StartNew(async () =>
        {
            while (!await CheckIfTopicExists(eventName))
            {
                _logger.LogTrace("There is not topic for event {EventName}", eventName);
                await Task.Delay(1000 * 60);
            }

            _consumer.Subscribe(ProcessEventNamePattern(eventName));
            _logger.LogTrace("Starting Kafka consume for event {EventName}", eventName);

            while (true)
            {
                try
                {
                    var consumeResult = _consumer.Consume();
                    if (consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }
                    var eventType = SubscriptionsManager.GetEventTypeByName(eventName);
                    var value = _kafkaSerilazer.Deserilaze(consumeResult.Message.Value, eventType);
                    await ProcessEvent(eventName, value!);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "An error occured processing event: {eventName}", eventName);
                }
            }
        }, TaskCreationOptions.LongRunning);
    }

    private async Task<bool> CheckIfTopicExists(string eventName)
    {
        var metadata = _adminClient.GetMetadata(TimeSpan.FromSeconds(10));
        var topicsMetadata = metadata.Topics;
        var processedEventName = eventName.StripSuffix(EventBusOptions.EventNameSuffixToRemove);
        var pattern = $"{EventBusOptions.ProjectName}.*.{processedEventName}";
        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var isTopicExists = topicsMetadata.Any(topic => regex.IsMatch(topic.Topic));
        return await Task.FromResult(isTopicExists);
    }
}
