using DL;
using DL.Concrete;
using DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Media;Trusted_Connection=True;");
            });

            return services;
        }
    }
}
