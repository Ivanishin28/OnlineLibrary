using BookContext.Contract.Events;
using MediatR;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.EventHandlers
{
    public class BookDeletedEventHandler : INotificationHandler<BookDeletedEvent>
    {
        private IUnitOfWork _unitOfWork;
        private IShelvedBookRepository _shelvedBookRepository;

        public BookDeletedEventHandler(IUnitOfWork unitOfWork, IShelvedBookRepository shelvedBookRepository)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            var bookId = new BookId(notification.BookId);
            var shelvedBooks = await _shelvedBookRepository.GetBy(bookId);
            if (!shelvedBooks.Any())
            {
                return;
            }

            foreach(var shelvedBook in shelvedBooks)
            {
                _shelvedBookRepository.Remove(shelvedBook);
            }

            await _unitOfWork.SaveChanges();
        }
    }
}
