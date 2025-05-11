using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BooksOnShelves;
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
        private IBookOnAShelfRepository _bookOnAShelfRepository;

        public ShelfService(IBookOnAShelfRepository bookOnAShelfRepository)
        {
            _bookOnAShelfRepository = bookOnAShelfRepository;
        }

        public async Task<Result> ShelveBook(Shelf shelf, Book book)
        {
            var isAlreadyShelved = await _bookOnAShelfRepository.Exists(shelf.Id, book.Id);

            if(isAlreadyShelved)
            {
                return Result.Failure(ShelfErrors.AlreadyShelved);
            }

            var bookOnAShelf = BookOnAShelf.Create(shelf.Id, book.Id);

            await _bookOnAShelfRepository.Add(bookOnAShelf);

            return Result.Success();
        }
    }
}
