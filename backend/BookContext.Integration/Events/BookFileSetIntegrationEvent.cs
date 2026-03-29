using MediatR;

namespace BookContext.Integration.Events;

public record BookFileSetIntegrationEvent(Guid BookId, Guid FileId);
