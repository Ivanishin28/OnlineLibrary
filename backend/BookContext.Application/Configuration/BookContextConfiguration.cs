using BookContext.Application.Controllers;
using BookContext.DL.SqlServer.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Application.Configuration
{
    public static class BookContextConfiguration
    {
        public static IServiceCollection RegisterBookContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .RegisterRepositories()
                .RegisterDbContext()
                .RegisterUseCases();

            return services;
        }

        public static IMvcBuilder AddBookContextControllers(this IMvcBuilder mvcBuilder)
        {
            var apiAssembly = typeof(AuthorController).Assembly;

            mvcBuilder
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(apiAssembly));

            return mvcBuilder;
        }
    }
}
