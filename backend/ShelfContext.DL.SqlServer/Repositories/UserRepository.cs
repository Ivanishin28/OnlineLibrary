using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSet<User> _dbSet;

        public UserRepository(ShelfDbContext db)
        {
            _dbSet = db.Users;
        }

        public async Task<bool> Exists(UserId userId)
        {
            return await _dbSet
                .AnyAsync(user => user.Id == userId);
        }
    }
}
