using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Services
{
    public class ShelfService : IShelfService
    {
        private IShelvedBookRepository _shelvedBookRepository;

        public ShelfService(IShelvedBookRepository shelvedBookRepository)
        {
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result> ShelveBook(Shelf shelf, Book book)
        {
            var isAlreadyShelved = await _shelvedBookRepository.Exists(shelf.Id, book.Id);

            if(isAlreadyShelved)
            {
                return Result.Failure(ShelfErrors.AlreadyShelved);
            }

            var shelvedBook = ShelvedBook.Create(shelf.Id, book.Id);

            await _shelvedBookRepository.Add(shelvedBook);

            return Result.Success();
        }
    }
}
