using Microsoft.Extensions.DependencyInjection;
using UserContext.Contract.Commands.CreateUser;
using UserContext.UseCases.Commands;

namespace UserContext.UseCases.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(CreateUserRequest).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(CreateUserRequestHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
