using Composition.Application.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace ShelfContext.Application.Configuration
{
    public static class CompositionConfiguration
    {
        public static IServiceCollection RegisterComposition(this IServiceCollection services)
        {
            services
                .RegisterUseCases();

            return services;
        }

        public static IMvcBuilder AddCompositionControllers(this IMvcBuilder mvcBuilder)
        {
            var apiAssembly = typeof(LibraryController).Assembly;

            mvcBuilder
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(apiAssembly));

            return mvcBuilder;
        }
    }
}
