using Microsoft.Extensions.DependencyInjection;

namespace MediaContext.Application.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(UseCasesConfiguration).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(UseCasesConfiguration).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
