using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Concrete
{
    public class IdentityDbInitializer : IDbInitializer
    {
        private ApplicationIdentityDbContext _db;

        public IdentityDbInitializer(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public Task Initialize()
        {
            return _db.Database.MigrateAsync();
        }
    }
}
