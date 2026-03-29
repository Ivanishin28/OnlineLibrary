using BookContext.Integration.Events;
using DL;
using IdentityContext.Integration.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BL.IntegrationEventConsumers.Consumers;

public class FileRemovedConsumer : 
    IConsumer<BookCoverRemovedIntegrationEvent>,
    IConsumer<BookFileRemovedIntegrationEvent>,
    IConsumer<AuthorAvatarRemovedIntegrationEvent>,
    IConsumer<UserAvatarRemovedIntegrationEvent>
{
    private MediaDbContext _db;

    public FileRemovedConsumer(MediaDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<BookCoverRemovedIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<BookFileRemovedIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<AuthorAvatarRemovedIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<UserAvatarRemovedIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    private async Task Handle(Guid fileId)
    {
        var file = await _db
            .MediaFiles
            .FirstOrDefaultAsync(x => x.Id == fileId);
        if (file == null)
        {
            return;
        }

        file.MarkAsUnused();

        await _db.SaveChangesAsync();
    }
}
