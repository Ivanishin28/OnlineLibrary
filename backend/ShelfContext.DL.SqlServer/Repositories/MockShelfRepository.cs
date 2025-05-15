using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class MockShelfRepository : IShelfRepository
    {
        public Task Add(Shelf shelf)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Shelf shelf)
        {
            throw new NotImplementedException();
        }

        public Task<Shelf> GetBy(ShelfId id)
        {
            throw new NotImplementedException();
        }

        public Task<Shelf> GetBy(BookId bookId)
        {
            throw new NotImplementedException();
        }
    }
}
