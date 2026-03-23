namespace IdentityContext.Integration.Events;

public record UserAvatarRemovedIntegrationEvent(Guid UserId, Guid FileId);
