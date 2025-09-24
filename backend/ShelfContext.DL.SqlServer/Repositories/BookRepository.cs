using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private ShelfDbContext _db;

        public BookRepository(ShelfDbContext db)
        {
            _db = db;
        }

        public Task<Book?> GetBy(BookId id)
        {
            return _db.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
