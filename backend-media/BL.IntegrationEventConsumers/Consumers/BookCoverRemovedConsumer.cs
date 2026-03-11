using BookContext.Integration.Events;
using MassTransit;

namespace BL.IntegrationEventConsumers.Consumers;

public class BookCoverRemovedConsumer : IConsumer<BookCoverRemovedIntegrationEvent>
{
    public Task Consume(ConsumeContext<BookCoverRemovedIntegrationEvent> context)
    {
        Console.WriteLine($"Consume file {context.Message.FileId}");

        return Task.CompletedTask;
    }
}
