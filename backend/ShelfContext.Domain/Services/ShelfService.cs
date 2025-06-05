using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfService : IShelfService
    {
        private IShelvedBookRepository _shelvedBookRepository;

        public ShelfService(IShelvedBookRepository shelvedBookRepository)
        {
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result<ShelvedBook>> ShelveBook(ShelfId shelfId, BookId bookId)
        {
            var isAlreadyShelved = await _shelvedBookRepository.Exists(shelfId, bookId);

            if(isAlreadyShelved)
            {
                return Result<ShelvedBook>.Failure(ShelfErrors.AlreadyShelved);
            }

            return ShelvedBook.Create(shelfId, bookId);
        }
    }
}
