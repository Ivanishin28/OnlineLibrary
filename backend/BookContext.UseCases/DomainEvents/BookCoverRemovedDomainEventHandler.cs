using BookContext.Domain.DomainEvents;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverRemovedDomainEventHandler : INotificationHandler<BookCoverRemovedDomainEvent>
{
    public Task Handle(BookCoverRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
