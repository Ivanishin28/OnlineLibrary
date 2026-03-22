using BookContext.Integration.Events;
using DL;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BL.IntegrationEventConsumers.Consumers;

public class BookCoverRemovedConsumer : IConsumer<BookContext.Integration.Events.BookCoverRemovedIntegrationEvent>
{
    private MediaDbContext _db;

    public BookCoverRemovedConsumer(MediaDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<BookCoverRemovedIntegrationEvent> context)
    {
        var file = await _db
            .MediaFiles
            .FirstOrDefaultAsync(x => x.Id == context.Message.FileId);
        if (file == null)
        {
            return;
        }

        file.MarkAsUnused();

        await _db.SaveChangesAsync();
    }
}
