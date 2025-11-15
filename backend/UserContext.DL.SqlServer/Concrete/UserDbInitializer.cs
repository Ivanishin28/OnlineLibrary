using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.DL.SqlServer.Concrete
{
    public class UserDbInitializer : IDbInitializer
    {
        private UserDbContext _db;

        public UserDbInitializer(UserDbContext db)
        {
            _db = db;
        }

        public Task Initialize()
        {
            return _db.Database.MigrateAsync();
        }
    }
}
