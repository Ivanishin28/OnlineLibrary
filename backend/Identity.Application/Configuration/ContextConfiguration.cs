using IdentityContext.Application.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityContext.Application.Configuration
{
    public static class ContextConfiguration
    {
        public static IServiceCollection RegisterIdentityContext(
            this IServiceCollection services, 
            ConfigurationManager config)
        {
            services
                .ConfigureIdentity()
                .RegisterServices()
                .RegisterDataLayer()
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
