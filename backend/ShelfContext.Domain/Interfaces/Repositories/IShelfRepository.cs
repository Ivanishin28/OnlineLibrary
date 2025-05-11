using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IShelfRepository
    {
        Task<Shelf> GetBy(ShelfId id);
        Task<Shelf> GetBy(BookId bookId);
        Task<bool> IsShelved(BookId bookId);

        Task Add(Shelf shelf);
        Task Delete(Shelf shelf);
    }
}
