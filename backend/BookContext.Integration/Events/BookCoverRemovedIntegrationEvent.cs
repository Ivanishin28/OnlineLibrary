using MediatR;

namespace BookContext.Integration.Events;

public record BookCoverRemovedIntegrationEvent(Guid BookId, Guid FileId) : INotification
{
}
