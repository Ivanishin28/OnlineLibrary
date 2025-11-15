using IdentityContext.DL;
using IdentityContext.DL.Concrete;
using IdentityContext.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.DL.Interfaces;

namespace IdentityContext.Application.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            return services
                .RegisterDbQueries()
                .RegisterDbContext();
        }

        internal static IServiceCollection RegisterDbQueries(this IServiceCollection services)
        {
            services
                .AddTransient<IDbInitializer, IdentityDbInitializer>()
                .AddTransient<IIdentityChecker, IdentityChecker>();

            return services;
        }

        internal static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");
            });

            return services;
        }
    }
}
