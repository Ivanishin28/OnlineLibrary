using BookContext.Domain.DomainEvents;
using BookContext.Domain.Interfaces;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Entities
{
    public class BookMetadata : Entity
    {
        public BookMetadataId Id { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public DateOnly PublishingDate { get; private set; }
        public MediaFileId? CoverId { get; private set; }
        public MediaFileId? FileId { get; private set; }
        public BookDescription? Description { get; private set; }

        private BookMetadata() { }

        public BookMetadata(
            BookId bookId, 
            DateOnly publishingDate, 
            MediaFileId? coverId, 
            BookDescription? description, 
            MediaFileId? fileId)
        {
            Id = new BookMetadataId(Guid.NewGuid());
            BookId = bookId;
            PublishingDate = publishingDate;
            CoverId = coverId;
            Description = description;
            FileId = fileId;
        }

        public void SetCover(MediaFileId? cover)
        {
            if (CoverId == cover)
            {
                return;
            }

            if (CoverId != null)
            {
                RaiseDomainEvent(new BookCoverRemoved(BookId, CoverId));
            }
            if (cover != null)
            {
                RaiseDomainEvent(new BookCoverSet(BookId, cover));
            }

            CoverId = cover;
        }

        public void SetDescription(BookDescription? description)
        {
            Description = description;
        }

        public void SetPublishingDate(DateOnly date)
        {
            PublishingDate = date;
        }

        public void SetFile(MediaFileId? file)
        {
            FileId = file;
        }
    }
}
