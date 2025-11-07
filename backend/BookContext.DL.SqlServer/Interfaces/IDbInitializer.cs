using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.Interfaces
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
