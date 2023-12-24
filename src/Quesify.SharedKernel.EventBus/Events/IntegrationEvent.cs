namespace Quesify.EventBus.Events;

public abstract class IntegrationEvent
{
    public Guid Id { get; private set; }

    public DateTime CreationDate { get; private set; }

    public IntegrationEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }
}
