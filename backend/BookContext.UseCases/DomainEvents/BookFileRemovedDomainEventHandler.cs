using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookFileRemovedDomainEventHandler : INotificationHandler<BookFileRemovedDomainEvent>
{
    private IBus _bus;

    public BookFileRemovedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(BookFileRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new BookFileRemovedIntegrationEvent(
            notification.BookId.Value, notification.FileId.Value);
        await _bus.Publish(integrationEvent, cancellationToken);
    }
}
