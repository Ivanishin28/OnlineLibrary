using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Application.Concrete;
using ShelfContext.Application.Controllers;
using ShelfContext.Application.Interfaces;

namespace ShelfContext.Application.Configuration
{
    public static class ShelfContextConfiguration
    {
        public static IServiceCollection RegisterShelfContext(this IServiceCollection services)
        {
            services
                .AddTransient<IResouceAuthChecker, ResouceAuthChecker>();

            services
                .RegisterDataAccess()
                .RegisterDomainServices()
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
