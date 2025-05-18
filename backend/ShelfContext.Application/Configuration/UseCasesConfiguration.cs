using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });

            return services;
        }
    }
}
