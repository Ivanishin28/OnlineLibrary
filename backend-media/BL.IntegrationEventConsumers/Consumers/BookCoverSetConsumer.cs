using BookContext.Integration.Events;
using DL;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BL.IntegrationEventConsumers.Consumers;

public class BookCoverSetConsumer : IConsumer<BookCoverSetIntegrationEvent>
{
    private MediaDbContext _db;

    public BookCoverSetConsumer(MediaDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<BookCoverSetIntegrationEvent> context)
    {
        var file = await _db
            .MediaFiles
            .FirstOrDefaultAsync(x => x.Id == context.Message.FileId);
        if (file == null)
        {
            return;
        }

        file.MarkAsInUse();

        await _db.SaveChangesAsync();
    }
}
