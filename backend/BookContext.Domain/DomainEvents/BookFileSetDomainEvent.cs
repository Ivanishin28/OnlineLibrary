using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public record BookFileSetDomainEvent(BookId BookId, MediaFileId FileId) : IDomainEvent
{
}
