using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IShelfRepository, ShelfRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IShelvedBookRepository, ShelvedBookRepository>();

            services.RegisterQueries();

            return services;
        }
    }
}
