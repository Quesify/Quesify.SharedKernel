using Quesify.SharedKernel.EventBus;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.EventBus.SubscriptionManagers;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        Action<EventBusOptions> configureEventBusOptions)
    {
        Guard.Against.Null(services, nameof(services));

        EventBusOptions eventBusOptions = new EventBusOptions();
        configureEventBusOptions(eventBusOptions);

        return services
            .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>()
            .AddSingleton(eventBusOptions);
    }
}