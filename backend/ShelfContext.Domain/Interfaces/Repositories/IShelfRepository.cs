using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
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

        Task Add(Shelf shelf);
        Task Delete(Shelf shelf);
    }
}
