using BookContext.DL.SqlServer.Concrete;
using BookContext.DL.SqlServer.Repositories;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.DL.Interfaces;

namespace BookContext.DL.SqlServer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IDbInitializer, BookDbInitializer>()
                .AddTransient<IUnitOfWork, UnitOfWork>();

            services
                .AddTransient<IGenreRepository, GenreRepository>()
                .AddTransient<IAuthorMetadataRepository, AuthorMetadataRepository>()
                .AddTransient<IBookMetadataRepository, BookMetadataRepository>()
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}
