using Shared.Core.Models;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfService : IShelfService
    {
        private IShelfRepository _shelfRepository;
        private IBookShelvedChecker _bookShelvedChecker;

        public ShelfService(IShelfRepository shelfRepository, IBookShelvedChecker bookShelvedChecker)
        {
            _shelfRepository = shelfRepository;
            _bookShelvedChecker = bookShelvedChecker;
        }

        public async Task<Result<ShelvedBook>> ShelveBook(ShelfId shelfId, BookId bookId)
        {
            var shelf = await _shelfRepository.GetBy(shelfId);

            if(shelf is null)
            {
                return Result<ShelvedBook>.Failure(EntityErrors.NotFound);
            }

            var isAlreadyShelved = await _bookShelvedChecker.IsBookShelvedBy(bookId, shelf.UserId);

            if(isAlreadyShelved)
            {
                return Result<ShelvedBook>.Failure(ShelfErrors.AlreadyShelved);
            }

            return ShelvedBook.Create(shelfId, bookId);
        }
    }
}
