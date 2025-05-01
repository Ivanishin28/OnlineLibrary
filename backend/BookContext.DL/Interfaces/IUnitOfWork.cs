using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
