using Quesify.EventBus.Events;

namespace Quesify.SharedKernel.EventBus.Abstractions;

public interface IIntegrationEventHandler
{
}

public interface IIntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent integrationEvent);
}
