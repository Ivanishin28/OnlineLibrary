using Microsoft.Extensions.DependencyInjection;
using UserContext.DL.SqlServer.Concrete;
using UserContext.DL.SqlServer.Repositories;
using UserContext.Domain.Interfaces;
using UserContext.Domain.Repositories;

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
