using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Configuration
{
    public static class ShelfReadConfiguration
    {
        public static IServiceCollection RegisterShelfReadDbContext(this IServiceCollection services)
        {
            services
                .AddDbContext<ShelfReadDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");

                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            return services;
        }
    }
}
