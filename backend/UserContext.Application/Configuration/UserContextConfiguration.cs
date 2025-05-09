using BookContext.DL.SqlServer.Configuration;
using BookContext.UseCases.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Application.Controllers;

namespace UserContext.Application.Configuration
{
    public static class UserContextConfiguration
    {
        public static IServiceCollection RegisterUserContext(this IServiceCollection services)
        {
            services
                .RegisterRepositories()
                .RegisterDbContext()
                .RegisterUseCases();

            return services;
        }

        public static IMvcBuilder AddUserContextControllers(this IMvcBuilder mvcBuilder)
        {
            var apiAssembly = typeof(UserController).Assembly;

            mvcBuilder
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(apiAssembly));

            return mvcBuilder;
        }
    }
}
