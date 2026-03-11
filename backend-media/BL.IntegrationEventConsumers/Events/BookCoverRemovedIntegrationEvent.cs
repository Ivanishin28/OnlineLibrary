namespace BookContext.Integration.Events;

public record BookCoverRemovedIntegrationEvent
{
    public Guid BookId { get; set; }
    public Guid FileId { get; set; }

    public BookCoverRemovedIntegrationEvent() { }

    public BookCoverRemovedIntegrationEvent(Guid bookId, Guid fileId)
    {
        BookId = bookId;
        FileId = fileId;
    }
}
