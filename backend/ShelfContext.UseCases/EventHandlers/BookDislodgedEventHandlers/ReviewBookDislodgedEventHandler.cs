using MediatR;
using ShelfContext.Contract.Events;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.EventHandlers.BookDislodgedEventHandlers
{
    public class ReviewBookDislodgedEventHandler : INotificationHandler<BookDislodgedEvent>
    {
        private IReviewRepository _reviewRepository;
        private IUnitOfWork _unitOfWiork;

        public ReviewBookDislodgedEventHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWiork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWiork = unitOfWiork;
        }

        public async Task Handle(BookDislodgedEvent notification, CancellationToken cancellationToken)
        {
            var userId = new UserId(notification.UserId);
            var bookId = new BookId(notification.BookId);
            var review = await _reviewRepository.GetBy(userId, bookId);

            if (review is null)
            {
                return;
            }

            _reviewRepository.Remove(review);
            await _unitOfWiork.SaveChanges();
        }
    }
}
