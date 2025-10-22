using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class ShelvedBookRepository : IShelvedBookRepository
    {
        private ShelfDbContext _db;

        public ShelvedBookRepository(ShelfDbContext db)
        {
            _db = db;
        }

        public async Task<ShelvedBook?> GetBy(ShelvedBookId id)
        {
            return await GetShelvedBookAggregate()
                .Where(shelvedBook => shelvedBook.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ShelvedBook?> GetBy(UserId userId, BookId bookId)
        {
            return await GetShelvedBookAggregate()
                .Where(x => 
                    x.UserId == userId && 
                    x.BookId == bookId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<ShelvedBook>> GetBy(BookId bookId)
        {
            return await GetShelvedBookAggregate()
                .Where(x => x.BookId == bookId)
                .ToListAsync();
        }

        public void Add(ShelvedBook shelvedBook)
        {
            _db.ShelvedBooks.Add(shelvedBook);
        }

        public void Remove(ShelvedBook shelvedBook)
        {
            _db.ShelvedBooks.Remove(shelvedBook);
        }

        private IQueryable<ShelvedBook> GetShelvedBookAggregate()
        {
            return _db.ShelvedBooks.Include(shelvedBook => shelvedBook.BookTags);
        }
    }
}
