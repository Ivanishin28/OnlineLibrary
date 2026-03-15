using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverSetDomainEventHandler : INotificationHandler<BookCoverSetDomainEvent>
{
    private IBus _bus;

    public BookCoverSetDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(BookCoverSetDomainEvent notification, CancellationToken cancellationToken)
    {
        var @event = new BookCoverSetIntegrationEvent(
            notification.BookId.Value, notification.CoverId.Value);
        await _bus.Publish(@event, cancellationToken);
    }
}
