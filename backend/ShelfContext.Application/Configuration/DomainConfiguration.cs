using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Domain.Interfaces.Services;
using ShelfContext.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Configuration
{
    public static class DomainConfiguration
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IShelfNameCreationService, ShelfNameCreationService>();
            services.AddTransient<IShelfService, ShelfService>();

            return services;
        }
    }
}
