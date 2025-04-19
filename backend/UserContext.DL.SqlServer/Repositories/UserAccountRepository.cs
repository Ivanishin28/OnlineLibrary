using Microsoft.EntityFrameworkCore;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.DL.Repositories;
using UserContext.Domain.Entities;

namespace UserContext.DL.SqlServer.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private DbSet<UserAccount> _dbSet;

        public UserAccountRepository(UserDbContext db)
        {
            _dbSet = db.UserAccounts;
        }

        public async Task<Guid> Add(UserAccount account)
        {
            _dbSet.Add(account);
            return account.Id;
        }

        public async Task Delete(UserAccount account)
        {
            _dbSet.Remove(account);
        }

        public async Task<UserAccount> GetBy(Guid id)
        {
            return await _dbSet
                .Where(account => account.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsUnique(Email email)
        {
            return await _dbSet
                .AnyAsync(account => 
                    account.Email.Value == email.Value);
        }

        public async Task<bool> IsUnique(string login)
        {
            return await _dbSet
                .AnyAsync(account => 
                    account.Login == login);
        }
    }
}
