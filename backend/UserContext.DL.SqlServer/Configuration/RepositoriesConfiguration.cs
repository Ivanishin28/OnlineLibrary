using Microsoft.Extensions.DependencyInjection;
using UserContext.DL.Interfaces;
using UserContext.DL.Repositories;
using UserContext.DL.SqlServer.Concrete;
using UserContext.DL.SqlServer.Repositories;

namespace UserContext.DL.SqlServer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IUnitOfWork, UnitOfWork>();

            services
                .AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
