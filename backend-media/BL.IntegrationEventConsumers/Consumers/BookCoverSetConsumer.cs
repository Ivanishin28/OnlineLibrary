using BookContext.Integration.Events;
using MassTransit;

namespace BL.IntegrationEventConsumers.Consumers;

public class BookCoverSetConsumer : IConsumer<BookCoverSetIntegrationEvent>
{
    public Task Consume(ConsumeContext<BookCoverSetIntegrationEvent> context)
    {
        Console.WriteLine($"Consume file {context.Message.FileId}");

        return Task.CompletedTask;
    }
}
