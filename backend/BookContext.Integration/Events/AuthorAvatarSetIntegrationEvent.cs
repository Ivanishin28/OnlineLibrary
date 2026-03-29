using MediatR;

namespace BookContext.Integration.Events;

public record AuthorAvatarSetIntegrationEvent(Guid AuthorId, Guid FileId);
