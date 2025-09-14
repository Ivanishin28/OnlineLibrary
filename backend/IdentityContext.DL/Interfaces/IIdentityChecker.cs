using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Interfaces
{
    public interface IIdentityChecker
    {
        Task<bool> IsLoginTaken(string login);
        Task<bool> IsEmailTaken(string email);
    }
}
