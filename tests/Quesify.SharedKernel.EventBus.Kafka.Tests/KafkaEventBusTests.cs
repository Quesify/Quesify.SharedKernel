using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.EventBus.Kafka.Tests.Events;
using Quesify.SharedKernel.Json;
using Xunit;

namespace Quesify.SharedKernel.EventBus.Kafka.Tests;

public class KafkaEventBusTests
{
    public IServiceCollection Services { get; set; }

    public KafkaEventBusTests()
    {
        Services = new ServiceCollection();
        Services.AddLogging(o => o.AddConsole());
        Services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        Services.AddEventBus(eventBusOptions =>
        {
            eventBusOptions.ProjectName = "Quesify.SharedKernel";
            eventBusOptions.ServiceName = "Tests";
            eventBusOptions.EventNameSuffixToRemove = "IntegrationEvent";
            eventBusOptions.UseKafka(Services, kafkaOptions =>
            {
                kafkaOptions.ProducerConfig.BootstrapServers = "localhost:9092";
                kafkaOptions.ConsumerConfig.BootstrapServers = "localhost:9092";
                kafkaOptions.ConsumerConfig.GroupId = "Quesify.SharedKernel.Tests.Group";
                kafkaOptions.ConsumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
                kafkaOptions.ConsumerConfig.ClientId = "Quesify.SharedKernel.Tests";
                kafkaOptions.ConsumerConfig.AllowAutoCreateTopics = true;
                kafkaOptions.AdminClientConfig.AllowAutoCreateTopics = true;
                kafkaOptions.AdminClientConfig.BootstrapServers = "localhost:9092";
            });
        });
        Services.AddJsonSerializer(jsonSerializerOptions =>
        {
            jsonSerializerOptions.UseNewtonsoft(Services, jsonSerializerSettings => { });
        });
        Services.AddTransient<CategoryCreatedIntegrationEventHandler>();
    }

    [Fact]
    public async Task PublishAsync_ShouldSendAMessageToKafka()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();
        await eventBus.PublishAsync(new CategoryCreatedIntegrationEvent(Guid.NewGuid(), Guid.NewGuid(), "Technology", DateTime.Now));
    }

    [Fact]
    public async Task SubscribeAsync_ShouldSubscribeToAMessageOnKafka()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();
        await eventBus.SubscribeAsync<CategoryCreatedIntegrationEvent, CategoryCreatedIntegrationEventHandler>();
        await Task.Delay(1000 * 30);
    }
}
