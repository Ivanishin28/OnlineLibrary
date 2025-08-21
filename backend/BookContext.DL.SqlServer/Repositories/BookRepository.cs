using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private DbSet<Book> _dbSet;

        public BookRepository(BookDbContext db)
        {
            _dbSet = db.Books;
        }

        public async Task Add(Book book)
        {
            _dbSet.Add(book);
        }

        public async Task Delete(Book book)
        {
            _dbSet.Remove(book);
        }

        public async Task<Book?> GetBy(Guid id)
        {
            return await BookAggregates()
                .Where(book => book.Id == id)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Book> BookAggregates()
        {
            return _dbSet
                .Include(book => book.BookAuthors);
        }
    }
}
