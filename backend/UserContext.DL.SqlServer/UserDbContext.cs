using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UserContext.DL.SqlServer.EntityTypeConfigurations;
using UserContext.Domain.Entities;

namespace UserContext.DL.SqlServer
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public UserDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var currentAssembly = typeof(UserAccountEntityTypeConfiguration).Assembly;
            builder.ApplyConfigurationsFromAssembly(currentAssembly);
        }
    }
}
