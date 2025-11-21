using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaContext.DL.Concrete
{
    public class MediaDbInitializer : IDbInitializer
    {
        private MediaDbContext _db;

        public MediaDbInitializer(MediaDbContext db)
        {
            _db = db;
        }

        public Task Initialize()
        {
            return _db.Database.MigrateAsync();
        }
    }
}
