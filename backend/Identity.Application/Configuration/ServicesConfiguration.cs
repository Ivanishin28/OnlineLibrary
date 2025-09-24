using IdentityContext.Application.Concrete;
using IdentityContext.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityContext.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITokenBuilder, JWTTokenBuilder>();

            return services;
        }
    }
}
