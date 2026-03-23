using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class AuthorAvatarSetDomainEventHandler : INotificationHandler<AuthorAvatarSetDomainEvent>
{
    private IBus _bus;

    public AuthorAvatarSetDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(AuthorAvatarSetDomainEvent notification, CancellationToken cancellationToken)
    {
        var @event = new AuthorAvatarSetIntegrationEvent(
            notification.AuthorId.Value, notification.AvatarId.Value);
        await _bus.Publish(@event, cancellationToken);
    }
}
