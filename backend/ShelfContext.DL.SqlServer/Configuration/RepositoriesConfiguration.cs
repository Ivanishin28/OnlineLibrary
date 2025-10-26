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
                .AddTransient<IBookAccessor, BookAccessor>()
                .AddTransient<IShelfRepository, ShelfRepository>()
                .AddTransient<ITagRepository, TagRepository>()
                .AddTransient<IShelvedBookRepository, ShelvedBookRepository>();

            return services;
        }
    }
}
