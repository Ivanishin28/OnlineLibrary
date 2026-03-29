using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.DomainEvents;

public sealed record AuthorAvatarSetDomainEvent(AuthorId AuthorId, MediaFileId AvatarId) : IDomainEvent
{
}
