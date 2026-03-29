using MediatR;

namespace BookContext.Integration.Events;

public record BookFileRemovedIntegrationEvent(Guid BookId, Guid FileId);
