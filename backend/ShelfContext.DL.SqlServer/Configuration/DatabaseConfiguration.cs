using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.DL.Interfaces;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ShelfDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineLibrary;Trusted_Connection=True;");
            });

            services
                .AddTransient<IDbInitializer, ShelfDbInitializer>()
                .AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
