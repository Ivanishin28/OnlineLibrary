using IdentityContext.DL.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Entities.ApplicationUser
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid UserId { get; set; }

        public ApplicationUser() : base() { }
    }
}
