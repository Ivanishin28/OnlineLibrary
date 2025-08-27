using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityContext.DL
{
    public class ApplicationIdentityDbContext 
        : IdentityDbContext<
            ApplicationUser, 
            IdentityRole<Guid>, 
            Guid>
    {
        public ApplicationIdentityDbContext(
            DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .ToTable("ApplicationUser");
        }
    }
}
