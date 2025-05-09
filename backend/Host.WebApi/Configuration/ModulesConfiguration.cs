using BookContext.Application.Configuration;
using UserContext.Application.Configuration;

namespace Host.WebApi.Configuration
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModuleServices(this IServiceCollection services, ConfigurationManager config)
        {
            services
                .RegisterBookContext(config)
                .RegisterUserContext();

            return services;
        }

        public static IMvcBuilder AddModuleControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder
                .AddBookContextControllers()
                .AddUserContextControllers();
        }
    }
}
