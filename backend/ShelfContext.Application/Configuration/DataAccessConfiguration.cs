using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.Read.Configuration;
using ShelfContext.DL.SqlServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Configuration
{
    internal static class DataAccessConfiguration
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services)
        {
            return services
                .RegisterReadSide()
                .RegisterWriteSide();
        }

        private static IServiceCollection RegisterWriteSide(this IServiceCollection services)
        {
            return services
                .RegisterDbContext()
                .RegisterRepositories();
        }

        private static IServiceCollection RegisterReadSide(this IServiceCollection services)
        {
            return services
                .RegisterShelfReadDbContext();
        }
    }
}
