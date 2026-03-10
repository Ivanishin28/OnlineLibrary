using BookContext.Domain.DomainEvents;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverSetDomainEventHandler : INotificationHandler<BookCoverSetDomainEvent>
{
    public Task Handle(BookCoverSetDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
