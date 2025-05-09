using BookContext.Contract.Commands.CreateAuthor;
using BookContext.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BookContext.UseCases.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(CreateAuthorRequest).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(CreateAuthorRequestHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
