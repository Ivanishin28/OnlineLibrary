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
                .Join(_db.Shelves, x => x.ShelfId, x => x.Id, (book, shelf) => new { book, shelf })
                .Where(x => x.shelf.UserId == userId && x.book.BookId == bookId)
                .Select(x => x.book)
                .FirstOrDefaultAsync();
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
