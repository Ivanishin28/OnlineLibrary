using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.SqlServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext _db;

        public BookRepository(BookDbContext db)
        {
            _db = db;
        }

        public void Add(Book book)
        {
            _db.Books.Add(book);
        }

        public void Delete(Book book)
        {
            _db.Books.Remove(book);
        }

        public async Task<Book?> GetBy(Guid id)
        {
            return await BookAggregates()
                .Where(book => book.Id.Value == id)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Book> BookAggregates()
        {
            return _db.Books;
        }
    }
}
