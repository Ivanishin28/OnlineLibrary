using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public record BookCoverRemoved(BookId BookId, MediaFileId CoverId) : IDomainEvent
{
}
