using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.DL.Read.Queries;
using ShelfContext.UseCases.Commands;

namespace ShelfContext.Application.Configuration
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(CreateTagRequest).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(CreateTagRequestHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);

                var readsideHandlers = typeof(GetShelvesByUserIdRequestHandler).Assembly;
                config.RegisterServicesFromAssembly(readsideHandlers);
            });

            return services;
        }
    }
}
