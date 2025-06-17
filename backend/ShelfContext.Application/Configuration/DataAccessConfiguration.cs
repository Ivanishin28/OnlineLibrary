using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.Read.Configuration;
using ShelfContext.DL.SqlServer.Configuration;

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
                .RegisterReadDbContext()
                .RegisterQueries();
        }
    }
}
