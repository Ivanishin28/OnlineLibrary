using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverRemovedDomainEventHandler : INotificationHandler<BookCoverRemovedDomainEvent>
{
    private IBus _bus;

    public BookCoverRemovedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(BookCoverRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new BookCoverRemovedIntegrationEvent(
            notification.BookId.Value, notification.CoverId.Value);
        await _bus.Publish(integrationEvent, cancellationToken);
    }
}
