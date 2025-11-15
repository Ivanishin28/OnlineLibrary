using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Concrete
{
    public class ShelfDbInitializer : IDbInitializer
    {
        private ShelfDbContext _db;

        public ShelfDbInitializer(ShelfDbContext db)
        {
            _db = db;
        }

        public Task Initialize()
        {
            return _db.Database.MigrateAsync();
        }
    }
}
