using BookContext.Domain.DomainEvents;
using BookContext.Integration.Events;
using MassTransit;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class AuthorAvatarRemovedDomainEventHandler : INotificationHandler<AuthorAvatarRemovedDomainEvent>
{
    private IBus _bus;

    public AuthorAvatarRemovedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(AuthorAvatarRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new AuthorAvatarRemovedIntegrationEvent(
            notification.AuthorId.Value, notification.AvatarId.Value);
        await _bus.Publish(integrationEvent, cancellationToken);
    }
}
