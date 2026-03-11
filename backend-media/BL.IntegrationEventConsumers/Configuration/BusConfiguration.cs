using BL.IntegrationEventConsumers.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BL.IntegrationEventConsumers.Configuration;

public static class BusConfiguration
{
    public static IServiceCollection RegisterBus(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MassTransitHostOptions>(options =>
        {
            options.StartTimeout = TimeSpan.FromSeconds(10);
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(BookCoverRemovedConsumer).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
