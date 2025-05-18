using BookContext.Application.Configuration;
using ShelfContext.Application.Configuration;
using UserContext.Application.Configuration;

namespace Host.WebApi.Configuration
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModuleServices(this IServiceCollection services, ConfigurationManager config)
        {
            services
                .RegisterUserContext()
                .RegisterBookContext(config)
                .RegisterShelfContext();

            return services;
        }

        public static IMvcBuilder AddModuleControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder
                .AddUserContextControllers()
                .AddBookContextControllers()
                .AddShelfContextControllers();
        }
    }
}
