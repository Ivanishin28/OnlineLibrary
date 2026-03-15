using BL.IntegrationEventConsumers.Configuration;

namespace MediaContext.Application.Configuration
{
    public static class ContextConfiguration
    {
        public static IServiceCollection RegisterMediaContext(
            this IServiceCollection services,
            ConfigurationManager config)
        {
            services
                .RegisterBus(config)
                .AddTransient<BackendMedia.Application>()
                .RegisterDataLayer()
                .RegisterUseCases();

            return services;
        }
    }
}
