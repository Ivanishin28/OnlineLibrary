using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public record BookCoverRemovedDomainEvent(BookId BookId, MediaFileId CoverId) : IDomainEvent
{
}
