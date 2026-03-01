using IdentityContext.Application.Concrete;
using IdentityContext.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;

namespace IdentityContext.Application.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITokenBuilder, JwtTokenBuilder>()
                .AddTransient<IUserContext, HttpUserContext>();

            return services;
        }
    }
}
