using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public record AuthorAvatarSetDomainEvent(AuthorId AuthorId, MediaFileId AvatarId) : IDomainEvent
{
}
