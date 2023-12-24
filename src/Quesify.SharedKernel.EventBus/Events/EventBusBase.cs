using Quesify.SharedKernel.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quesify.EventBus.Events;

namespace Quesify.SharedKernel.EventBus.Events;

public abstract class EventBusBase : IEventBus
{
    protected readonly IEventBusSubscriptionsManager SubscriptionsManager;

    protected readonly IServiceProvider ServiceProvider;

    protected readonly EventBusOptions EventBusOptions;

    private readonly ILogger<EventBusBase> _logger;

    public EventBusBase(
        IEventBusSubscriptionsManager subscriptionsManager,
        IServiceProvider serviceProvider,
        EventBusOptions eventBusOptions)
    {
        SubscriptionsManager = subscriptionsManager;
        ServiceProvider = serviceProvider;
        EventBusOptions = eventBusOptions;
        _logger = serviceProvider.GetRequiredService<ILogger<EventBusBase>>();
    }

    public abstract Task PublishAsync(IntegrationEvent integrationEvent);

    public abstract Task SubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    public abstract Task UnsubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    protected async Task ProcessEvent(string eventName, object integrationEvent)
    {
        _logger.LogTrace("Processing Kafka event: {EventName}", eventName);

        if (!SubscriptionsManager.HasSubscriptionsForEvent(eventName))
        {
            _logger.LogWarning("No subscription for Kafka event: {EventName}", eventName);
            return;
        }

        var subscriptions = SubscriptionsManager.GetHandlersForEvent(eventName);
        using var serviceScope = ServiceProvider.CreateScope();

        foreach (var subscription in subscriptions)
        {
            var handler = serviceScope.ServiceProvider.GetRequiredService(subscription.HandlerType);
            if (handler == null)
            {
                continue;
            }

            var eventType = SubscriptionsManager.GetEventTypeByName(eventName);
            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
            await (Task)concreteType.GetMethod("HandleAsync")?.Invoke(handler, [integrationEvent])!;
        }
    }

    protected virtual string ProcessEventName(string eventName)
    {
        return eventName
            .StripPrefix(EventBusOptions.EventNamePrefixToRemove)
            .StripSuffix(EventBusOptions.EventNameSuffixToRemove)
            .AppendToStart($"{EventBusOptions.ProjectName}.{EventBusOptions.ServiceName}.");
    }

    protected virtual string ProcessEventNamePattern(string eventName)
    {
        return eventName
            .StripPrefix(EventBusOptions.EventNamePrefixToRemove)
            .StripSuffix(EventBusOptions.EventNameSuffixToRemove)
            .AppendToStart($"^{EventBusOptions.ProjectName}.*.");
    }
}

