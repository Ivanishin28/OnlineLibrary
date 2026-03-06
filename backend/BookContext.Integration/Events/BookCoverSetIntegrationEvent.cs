using MediatR;

namespace BookContext.Integration.Events;

public record BookCoverSetIntegrationEvent(Guid BookId, Guid FileId) : INotification
{
}
