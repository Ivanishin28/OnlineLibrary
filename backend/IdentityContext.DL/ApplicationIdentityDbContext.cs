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

            var user = builder
                .Entity<ApplicationUser>();

            user
                .ToTable("ApplicationUsers");

            user
                .HasIndex(x => x.Email)
                .IsUnique();

            user
                .HasIndex(x => x.UserName)
                .IsUnique();
        }
    }
}
