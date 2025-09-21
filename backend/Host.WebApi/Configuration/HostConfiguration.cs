namespace Host.WebApi.Configuration
{
    public static class HostConfiguration
    {
        public static IServiceCollection RegisterHostServices(this IServiceCollection services)
        {
            services.AddSingleton<TimeProvider>(TimeProvider.System);

            return services;
        }
    }
}
