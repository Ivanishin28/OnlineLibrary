using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelvingService : IShelvingService
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IBookAccessor _bookAccessor;
        private readonly IShelvedBookRepository _shelvedBookRepository;

        public ShelvingService(
            IShelfRepository shelfRepository, 
            IBookAccessor bookAccessor, 
            IShelvedBookRepository shelvedBookRepository)
        {
            _shelfRepository = shelfRepository;
            _bookAccessor = bookAccessor;
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result<ShelvedBookId>> Shelve(ShelfId shelfId, BookId bookId)
        {
            var shelf = await _shelfRepository.GetBy(shelfId);

            if (shelf is null)
            {
                return Result<ShelvedBookId>.Failure(EntityErrors.NotFound);
            }

            var book = await _bookAccessor.GetBy(bookId);

            if (book is null)
            {
                return Result<ShelvedBookId>.Failure(EntityErrors.NotFound);
            }

            var shelvedBook = await _shelvedBookRepository.GetBy(shelf.UserId, book.Id);

            if (shelvedBook is null)
            {
                return ShelveNewBook(shelf, book);
            }
            else
            {
                return Reshelve(shelvedBook, shelf);
            }
        }

        private ShelvedBookId ShelveNewBook(Shelf shelf, Book book)
        {
            var shelvedBook = shelf.Shelve(book.Id);
            _shelvedBookRepository.Add(shelvedBook);
            return shelvedBook.Id;
        }

        private Result<ShelvedBookId> Reshelve(ShelvedBook shelvedBook, Shelf shelf)
        {
            var result = shelvedBook.ReshelveTo(shelf);

            if (result.IsFailure)
            {
                return result.ToFailure<ShelvedBookId>();
            }
            else
            {
                return shelvedBook.Id;
            }
        }
    }
}
