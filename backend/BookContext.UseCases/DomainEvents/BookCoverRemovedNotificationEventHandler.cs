using BookContext.Domain.DomainEvents;
using MediatR;

namespace BookContext.UseCases.DomainEvents;

public class BookCoverRemovedNotificationEventHandler : INotificationHandler<BookCoverRemoved>
{
    public Task Handle(BookCoverRemoved notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
