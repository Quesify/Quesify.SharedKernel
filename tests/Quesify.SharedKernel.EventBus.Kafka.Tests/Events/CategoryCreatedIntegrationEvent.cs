using Quesify.EventBus.Events;

namespace Quesify.SharedKernel.EventBus.Kafka.Tests.Events;

public class CategoryCreatedIntegrationEvent : IntegrationEvent
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public CategoryCreatedIntegrationEvent(
        Guid id, 
        Guid categoryId,
        string name,
        DateTime creationDate) 
        : base(id, creationDate)
    {
        CategoryId = categoryId;
        Name = name;
    }
}
