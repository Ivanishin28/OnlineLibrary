namespace IdentityContext.Integration.Events;

public record UserAvatarSetIntegrationEvent(Guid UserId, Guid FileId);
