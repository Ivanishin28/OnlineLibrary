using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookFileSetDomainEventHandler : INotificationHandler<BookFileSetDomainEvent>
{
    private IBus _bus;

    public BookFileSetDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(BookFileSetDomainEvent notification, CancellationToken cancellationToken)
    {
        var @event = new BookFileSetIntegrationEvent(
            notification.BookId.Value, notification.FileId.Value);
        await _bus.Publish(@event, cancellationToken);
    }
}
