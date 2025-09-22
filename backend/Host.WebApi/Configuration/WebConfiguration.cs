using Host.WebApi.Web.Providers;
using Shared.Core.Interfaces;

namespace Host.WebApi.Configuration
{
    public static class WebConfiguration
    {
        public static IServiceCollection RegisterWebServices(this IServiceCollection services)
        {
            services.AddTransient<IUserContext, HttpUserContext>();

            return services;
        }
    }
}
