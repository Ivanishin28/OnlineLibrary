namespace BookContext.Integration.Events;

public record BookCoverSetIntegrationEvent
{
    public Guid BookId { get; set; }
    public Guid FileId { get; set; }

    public BookCoverSetIntegrationEvent() { }

    public BookCoverSetIntegrationEvent(Guid bookId, Guid fileId)
    {
        BookId = bookId;
        FileId = fileId;
    }
}