using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookContext.DL.SqlServer.Configuration
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection RegisterUserContextDatabase(this IServiceCollection services)
        {
            services.AddDbContext<BookDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");
            });

            return services;
        }
    }
}
