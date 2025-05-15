using ShelfContext.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShelfDbContext _db;

        public UnitOfWork(ShelfDbContext db)
        {
            _db = db;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
