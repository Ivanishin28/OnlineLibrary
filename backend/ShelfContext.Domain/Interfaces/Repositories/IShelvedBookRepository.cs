using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IShelvedBookRepository
    {
        Task<ShelvedBook> GetBy(ShelvedBookId id);
        Task<bool> Exists(ShelfId shelfId, BookId bookId);

        Task Add(ShelvedBook shelvedBook);
        Task Remove(ShelvedBook shelvedBook);
    }
}
