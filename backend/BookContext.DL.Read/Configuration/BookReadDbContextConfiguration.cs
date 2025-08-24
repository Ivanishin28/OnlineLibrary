using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Configuration
{
    public static class BookReadDbContextConfiguration
    {
        public static IServiceCollection RegisterReadDbContext(this IServiceCollection services)
        {
            services
                .AddDbContext<BookReadDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");

                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            return services;
        }
    }
}
