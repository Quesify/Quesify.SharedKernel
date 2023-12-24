using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Quesify.SharedKernel.EventBus.Abstractions;
using System.Diagnostics;

namespace Quesify.SharedKernel.EventBus.Kafka.Tests.Events;

public class CategoryCreatedIntegrationEventHandler : IIntegrationEventHandler<CategoryCreatedIntegrationEvent>
{
    public async Task HandleAsync(CategoryCreatedIntegrationEvent integrationEvent)
    {
        Debug.WriteLine($"{typeof(CategoryCreatedIntegrationEvent).Name} is handling.");
        await Task.CompletedTask;
    }
}
