using BookContext.Integration.Events;
using MassTransit;

namespace BL.IntegrationEventConsumers.Consumers;

public class BookCoverRemovedConsumer : IConsumer<BookContext.Integration.Events.BookCoverRemovedIntegrationEvent>
{
    public Task Consume(ConsumeContext<BookCoverRemovedIntegrationEvent> context)
    {
        Console.WriteLine($"Remove file {context.Message.FileId}");

        return Task.CompletedTask;
    }
}
