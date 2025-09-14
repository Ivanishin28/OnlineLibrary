using IdentityContext.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Concrete
{
    public class IdentityChecker : IIdentityChecker
    {
        private ApplicationIdentityDbContext _db;

        public IdentityChecker(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _db
                .Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsLoginTaken(string login)
        {
            return await _db
                .Users
                .AnyAsync(x => x.UserName == login);
        }
    }
}
