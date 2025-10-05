using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class BookAccessor : IBookAccessor
    {
        private ShelfDbContext _db;

        public BookAccessor(ShelfDbContext db)
        {
            _db = db;
        }

        public Task<Book?> GetBy(BookId id)
        {
            return _db
                .Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
