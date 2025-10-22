using MediatR;

namespace BookContext.Contract.Events
{
    public record BookDeletedEvent(Guid BookId) : INotification
    {
    }
}
