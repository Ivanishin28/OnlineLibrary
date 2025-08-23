using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Contract.Queries.GetBook;
using BookContext.DL.Read.Queries;
using BookContext.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BookContext.Application.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            return services
                .RegisterReadSideUseCases()
                .RegisterCommands();
        }

        private static IServiceCollection RegisterCommands(this IServiceCollection services)
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

        private static IServiceCollection RegisterReadSideUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(GetFullBookQuery).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(GetFullBookQueryHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
