using BookContext.Integration.Events;
using DL;
using IdentityContext.Integration.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BL.IntegrationEventConsumers.Consumers;

public class FileSetConsumer : 
    IConsumer<BookCoverSetIntegrationEvent>,
    IConsumer<BookFileSetIntegrationEvent>,
    IConsumer<AuthorAvatarSetIntegrationEvent>,
    IConsumer<UserAvatarSetIntegrationEvent>
{
    private MediaDbContext _db;

    public FileSetConsumer(MediaDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<BookCoverSetIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<UserAvatarSetIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<AuthorAvatarSetIntegrationEvent> context)
    {
        await Handle(context.Message.FileId);
    }

    public async Task Consume(ConsumeContext<BookFileSetIntegrationEvent> context)
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

        file.MarkAsInUse();

        await _db.SaveChangesAsync();
    }
}
