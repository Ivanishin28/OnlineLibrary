using BookContext.Contract.Events;
using MediatR;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.EventHandlers.BookDeletedEventHandlers
{
    public class ReviewBookDeletionHandler : INotificationHandler<BookDeletedEvent>
    {
        private IReviewRepository _reviewRepository;
        private IUnitOfWork _unitOfWork;

        public ReviewBookDeletionHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            var bookId = new BookId(notification.BookId);
            var reviews = await _reviewRepository.GetAllBy(bookId);

            if (!reviews.Any())
            {
                return;
            }

            foreach (var review in reviews)
            {
                _reviewRepository.Remove(review);
            }
            await _unitOfWork.SaveChanges();
        }
    }
}
