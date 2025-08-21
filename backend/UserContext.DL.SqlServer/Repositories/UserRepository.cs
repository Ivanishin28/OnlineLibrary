using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.DL.Repositories;
using UserContext.Domain.Entities;

namespace UserContext.DL.SqlServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSet<User> _dbSet;

        public UserRepository(UserDbContext db)
        {
            _dbSet = db.Users;
        }

        public async Task Add(User userProfile)
        {
            _dbSet.Add(userProfile);
        }

        public async Task Delete(User userProfile)
        {
            _dbSet.Remove(userProfile);
        }

        public async Task<User?> GetBy(Guid id)
        {
            return await _dbSet
                .Where(profile => profile.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
