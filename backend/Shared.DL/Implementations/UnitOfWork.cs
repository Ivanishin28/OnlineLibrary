using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
