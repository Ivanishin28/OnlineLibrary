using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private DbSet<Shelf> _dbSet;

        public ShelfRepository(ShelfDbContext db)
        {
            _dbSet = db.Shelves;
        }

        public async Task Add(Shelf shelf)
        {
            _dbSet.Add(shelf);
        }

        public async Task Delete(Shelf shelf)
        {
            _dbSet.Remove(shelf);
        }

        public async Task<Shelf> GetBy(ShelfId id)
        {
            return await _dbSet
                .Where(shelf => shelf.Id.Value == id.Value)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameUniqueForUser(ShelfName shelfName, UserId userId)
        {
            return await _dbSet
                .AnyAsync(shelf => 
                    shelf.Name.Value == shelfName.Value &&
                    shelf.UserId.Value == userId.Value);
        }
    }
}
