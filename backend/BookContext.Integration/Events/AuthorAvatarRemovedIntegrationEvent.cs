using MediatR;

namespace BookContext.Integration.Events;

public record AuthorAvatarRemovedIntegrationEvent(Guid AuthorId, Guid FileId);
