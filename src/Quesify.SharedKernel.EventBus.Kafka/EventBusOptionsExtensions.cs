using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.EventBus.Kafka;
using Quesify.SharedKernel.Utilities.Guards;

namespace Quesify.SharedKernel.EventBus;

public static class EventBusOptionsExtensions
{
    public static EventBusOptions UseKafka(
        this EventBusOptions eventBusOptions,
        IServiceCollection services,
        Action<KafkaOptions> configureKafkaOptions)
    {
        Guard.Against.Null(eventBusOptions, nameof(eventBusOptions));
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureKafkaOptions, nameof(configureKafkaOptions));

        KafkaOptions kafkaOptions = new KafkaOptions();
        configureKafkaOptions(kafkaOptions);

        services
            .AddSingleton<IEventBus, KafkaEventBus>()
            .AddSingleton<IKafkaSerilazer, KafkaSerilazer>()
            .AddSingleton(kafkaOptions);

        return eventBusOptions;
    }
}
