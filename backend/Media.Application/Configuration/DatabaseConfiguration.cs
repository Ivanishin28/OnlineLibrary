using MediaContext.DL;
using MediaContext.DL.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.DL.Interfaces;

namespace MediaContext.Application.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            return services
                .AddTransient<IDbInitializer, MediaDbInitializer>()
                .RegisterDbContext();
        }

        internal static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MediaDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");
            });

            return services;
        }
    }
}
