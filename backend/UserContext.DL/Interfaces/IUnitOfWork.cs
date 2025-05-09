using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.DL.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
