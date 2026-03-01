using DL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DL.Concrete
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
