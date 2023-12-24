namespace Quesify.SharedKernel.Core.Entities;

public interface IAggregateRoot : IEntity
{
}

public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
{
}
