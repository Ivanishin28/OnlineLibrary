using BookContext.Domain.DomainEvents;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverSetDomainEventHandler : INotificationHandler<BookCoverSet>
{
    public Task Handle(BookCoverSet notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
