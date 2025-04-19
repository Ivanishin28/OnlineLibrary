using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Domain.Entities;

namespace UserContext.DL.SqlServer
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
