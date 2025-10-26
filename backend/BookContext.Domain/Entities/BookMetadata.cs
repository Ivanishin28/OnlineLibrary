using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Entities
{
    public class BookMetadata
    {
        public BookMetadataId Id { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public DateOnly PublishingDate { get; private set; }
        public MediaFileId? CoverId { get; private set; }
        public BookDescription? Description { get; private set; }

        private BookMetadata() { }

        public BookMetadata(BookId bookId, DateOnly publishingDate, MediaFileId? coverId, BookDescription? description)
        {
            Id = new BookMetadataId(Guid.NewGuid());
            BookId = bookId;
            PublishingDate = publishingDate;
            CoverId = coverId;
            Description = description;
        }
    }
}
