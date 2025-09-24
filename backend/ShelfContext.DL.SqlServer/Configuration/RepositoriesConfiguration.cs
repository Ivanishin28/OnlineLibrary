using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IShelfRepository, ShelfRepository>()
                .AddTransient<ITagRepository, TagRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IShelvedBookRepository, ShelvedBookRepository>();

            return services;
        }
    }
}
