using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private DbSet<Shelf> _dbSet;

        public ShelfRepository(ShelfDbContext db)
        {
            _dbSet = db.Shelves;
        }

        public void Add(Shelf shelf)
        {
            _dbSet.Add(shelf);
        }

        public void Delete(Shelf shelf)
        {
            _dbSet.Remove(shelf);
        }

        public async Task<Shelf?> GetBy(ShelfId id)
        {
            return await _dbSet
                .Where(shelf => shelf.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
