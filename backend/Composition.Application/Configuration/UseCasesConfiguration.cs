using Composition.Application.UseCases.Queries;
using Composition.Contract.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ShelfContext.Application.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(GetLibraryPageQuery).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(GetLibraryPageQueryHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
