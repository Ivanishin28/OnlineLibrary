using BookContext.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookDbContext _db;

        public UnitOfWork(BookDbContext db)
        {
            _db = db;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
