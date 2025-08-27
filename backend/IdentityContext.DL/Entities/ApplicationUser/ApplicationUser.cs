using IdentityContext.DL.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Entities.ApplicationUser
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ProfileCreationStatus Status { get; private set; }

        public ApplicationUser() : base()
        {
            Status = ProfileCreationStatus.Pending;
        }
    }
}
