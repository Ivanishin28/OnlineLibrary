using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Application.Controllers;
using ShelfContext.DL.SqlServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Configuration
{
    public static class ShelfContextConfiguration
    {
        public static IServiceCollection RegisterShelfContext(this IServiceCollection services)
        {
            services
                .RegisterDomainServices()
                .RegisterDbContext()
                .RegisterRepositories()
                .RegisterUseCases();

            return services;
        }

        public static IMvcBuilder AddShelfContextControllers(this IMvcBuilder mvcBuilder)
        {
            var apiAssembly = typeof(TagController).Assembly;

            mvcBuilder
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(apiAssembly));

            return mvcBuilder;
        }
    }
}
