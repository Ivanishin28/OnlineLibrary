using BookContext.Application.Configuration;
using IdentityContext.Application.Configuration;
using ShelfContext.Application.Configuration;
using UserContext.Application.Configuration;
using MediaContext.Application.Configuration;

namespace Host.WebApi.Configuration
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModuleServices(
            this IServiceCollection services, 
            ConfigurationManager config)
        {
            services
                .RegisterMediaContext(config)
                .RegisterComposition()
                .RegisterIdentityContext(config)
                .RegisterUserContext()
                .RegisterBookContext(config)
                .RegisterShelfContext();

            return services;
        }

        public static IMvcBuilder AddModuleControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder
                .AddMediaControllers()
                .AddCompositionControllers()
                .AddIdentityContextControllers()
                .AddUserContextControllers()
                .AddBookContextControllers()
                .AddShelfContextControllers();
        }
    }
}
