using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BooksOnShelves;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IBookOnAShelfRepository
    {
        Task GetBy(BookOnAShelfId id);
        Task<bool> Exists(ShelfId shelfId, BookId bookId);

        Task Add(BookOnAShelf bookOnAShelf);
        Task Remove(BookOnAShelf bookOnAShelf);
    }
}
