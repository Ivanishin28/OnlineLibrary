namespace BookContext.Domain.Interfaces;

public interface IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
