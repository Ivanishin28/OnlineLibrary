using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Domain.Interfaces;

namespace UserContext.DL.SqlServer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserDbContext _db;

        public UnitOfWork(UserDbContext db)
        {
            _db = db;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
