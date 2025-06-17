using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Interfaces.Queries.IsNameUniqueForUser;

namespace ShelfContext.DL.Read.Configuration
{
    public static class QueriesConfiguration
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                var contractsAssembly = typeof(IsNameUniqueForUserQuery).Assembly;
                config.RegisterServicesFromAssembly(contractsAssembly);

                var handlersAssembly = typeof(IsNameUniqueForUserQueryHandler).Assembly;
                config.RegisterServicesFromAssembly(handlersAssembly);
            });

            return services;
        }
    }
}
