using BookContext.Application.Configuration;

namespace Host.WebApi.Configuration
{
    public static class ModulesConfiguration
    {
        public static IServiceCollection RegisterModuleServices(this IServiceCollection services, ConfigurationManager config)
        {
            
            services
                .RegisterBookContext(config);

            return services;
        }

        public static IMvcBuilder AddModuleControllers(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder
                .AddBookContextControllers();
        }
    }
}
