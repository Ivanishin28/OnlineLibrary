using IdentityContext.Application.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityContext.Application.Configuration
{
    public static class ContextConfiguration
    {
        public static IServiceCollection RegisterIdentityContext(this IServiceCollection services)
        {
            services
                .RegisterDbContext()
                .RegisterUseCases();

            return services;
        }

        public static IMvcBuilder AddIdentityContextControllers(this IMvcBuilder mvcBuilder)
        {
            var apiAssembly = typeof(ApplicationUserController).Assembly;

            mvcBuilder
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(apiAssembly));

            return mvcBuilder;
        }
    }
}
