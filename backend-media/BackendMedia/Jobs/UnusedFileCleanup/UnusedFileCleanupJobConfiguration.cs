using Microsoft.Extensions.Options;
using Quartz;

namespace BackendMedia.Jobs.UnusedFileCleanup;

public class UnusedFileCleanupJobConfiguration : IConfigureOptions<QuartzOptions>
{
    private static readonly JobKey Key = new(nameof(UnusedFileCleanupJob));
    public void Configure(QuartzOptions options)
    {
        options
            .AddJob<UnusedFileCleanupJob>(x => x.WithIdentity(Key))
            .AddTrigger(o =>
                o.ForJob(Key)
                    .WithSimpleSchedule(s =>
                        s.WithIntervalInHours(24)
                            .RepeatForever()
                    )
                    .StartAt(GetNextRunTime())
            );
    }

    private DateTime GetNextRunTime()
    {
        var now = DateTime.UtcNow;
        var nextRun = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);
        return now >= nextRun 
            ? nextRun.AddDays(1) 
            : nextRun;
    }
}
