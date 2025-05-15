using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.SqlServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Configuration
{
    public static class ShelfContextConfiguration
    {
        public static IServiceCollection RegisterShelfContext(this IServiceCollection services)
        {
            services
                .RegisterDbContext()
                .RegisterRepositories();

            return services;
        }
    }
}
