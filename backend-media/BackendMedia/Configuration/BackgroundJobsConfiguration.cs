using BackendMedia.Jobs.UnusedFileCleanup;
using Quartz;
using Quartz.AspNetCore;

namespace BackendMedia.Configuration;

public static class BackgroundJobsConfiguration
{
    public static IServiceCollection RegisterBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzServer(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<UnusedFileCleanupJobConfiguration>();

        return services;
    }
}
