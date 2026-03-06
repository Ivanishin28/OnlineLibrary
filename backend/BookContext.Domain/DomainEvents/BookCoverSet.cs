using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public record BookCoverSet(BookId BookId, MediaFileId CoverId) : IDomainEvent
{
}
