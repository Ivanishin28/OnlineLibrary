using MediaContext.DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediaContext.Application.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            return services
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
