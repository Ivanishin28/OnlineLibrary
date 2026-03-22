using DL;
using DL.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BackendMedia.Jobs.UnusedFileCleanup;

public class UnusedFileCleanupJob : IJob
{
    private MediaDbContext _db;

    public UnusedFileCleanupJob(MediaDbContext db)
    {
        _db = db;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cutOff = DateTime.UtcNow.AddDays(-1);
        await _db
            .MediaFiles
            .Where(x => 
                x.Status == FileStatus.Unused ||
                (x.Status == FileStatus.Pending && x.UploadTime <= cutOff))
            .ExecuteDeleteAsync(context.CancellationToken);
    }
}
