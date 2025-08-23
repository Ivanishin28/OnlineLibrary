using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class ShelvedBookRepository : IShelvedBookRepository
    {
        private DbSet<ShelvedBook> _dbSet;

        public ShelvedBookRepository(ShelfDbContext db)
        {
            _dbSet = db.ShelvedBooks;
        }

        public void Add(ShelvedBook shelvedBook)
        {
            _dbSet.Add(shelvedBook);
        }

        public async Task<ShelvedBook?> GetBy(ShelvedBookId id)
        {
            return await GetShelvedBookAggregate()
                .Where(shelvedBook => shelvedBook.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Remove(ShelvedBook shelvedBook)
        {
            _dbSet.Remove(shelvedBook);
        }

        private IQueryable<ShelvedBook> GetShelvedBookAggregate()
        {
            return _dbSet.Include(shelvedBook => shelvedBook.BookTags);
        }
    }
}
