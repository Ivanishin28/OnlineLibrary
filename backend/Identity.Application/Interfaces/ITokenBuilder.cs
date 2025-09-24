using IdentityContext.Application.Models;
using IdentityContext.DL.Entities.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Interfaces
{
    public interface ITokenBuilder
    {
        Token BuildFor(ApplicationUser user);
    }
}
