using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.SqlServer.Queries;
using ShelfContext.Domain.Interfaces.Queries.IsNameUniqueForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Configuration
{
    internal static class QueriesConfiguration
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
