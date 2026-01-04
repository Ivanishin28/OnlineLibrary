using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaContext.Application.Configuration
{
    public static class ContextConfiguration
    {
        public static IServiceCollection RegisterMediaContext(
            this IServiceCollection services,
            ConfigurationManager config)
        {
            services
                .RegisterDataLayer()
                .RegisterUseCases();

            return services;
        }
    }
}
