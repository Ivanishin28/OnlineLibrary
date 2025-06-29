using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;

namespace ShelfContext.DL.Read.Configuration
{
    public static class QueriesConfiguration
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(IsBookShelvedForUserQuery).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(IsBookShelvedForUserQueryHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
