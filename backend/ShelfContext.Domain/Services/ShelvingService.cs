using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelvingService : IShelvingService
    {
        public Result<ShelvedBook> Shelve(Shelf shelf, Book book)
        {
            if (!book.IsAccessibleTo(shelf.UserId))
            {
                return Result<ShelvedBook>.Failure(BookErrors.CANNOT_ACCESS_BOOK);
            }

            return shelf.Shelve(book.Id);
        }

        public Result Reshelve(ShelvedBook book, Shelf shelf)
        {
            if (book.UserId != shelf.UserId)
            {
                return Result.Failure(ShelvedBookErrors.RESHELVE_TO_OTHER_USER);
            }

            book.ReshelveTo(shelf);

            return Result.Success();
        }
    }
}
