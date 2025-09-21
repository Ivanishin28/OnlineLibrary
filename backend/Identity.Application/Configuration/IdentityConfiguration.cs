using IdentityContext.DL;
using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityContext.Application.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.DisableRequirements();
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddSignInManager();

            return services;
        }

        private static void DisableRequirements(this PasswordOptions options)
        {
            options.RequireDigit = false;
            options.RequireLowercase = false;
            options.RequireUppercase = false;
            options.RequireNonAlphanumeric = false;
            options.RequireDigit = false;
        }
    }
}
