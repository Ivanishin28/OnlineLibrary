using BookContext.DL.SqlServer.Concrete;
using BookContext.DL.SqlServer.Repositories;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}
