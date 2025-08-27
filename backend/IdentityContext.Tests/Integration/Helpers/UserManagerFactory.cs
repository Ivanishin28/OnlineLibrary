using IdentityContext.DL;
using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Tests.Integration.Helpers
{
    public static class UserManagerFactory
    {
        public static UserManager<ApplicationUser> CreateFor(ApplicationIdentityDbContext db)
        {
            var userStore = new UserStore<
                ApplicationUser,
                IdentityRole<Guid>,
                ApplicationIdentityDbContext, Guid>(db);

            return new UserManager<ApplicationUser>(
                userStore,
                null!,
                new PasswordHasher<ApplicationUser>(),
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                null!,
                null!,
                null!,
                null!
            );
        }
    }
}
