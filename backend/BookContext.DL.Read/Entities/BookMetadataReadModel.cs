using System;

namespace BookContext.DL.Read.Entities
{
    public class BookMetadataReadModel
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public DateOnly PublishingDate { get; private set; }
        public Guid? CoverId { get; private set; }
        public Guid? FileId { get; private set; }
        public string? Description { get; private set; }

        public BookReadModel Book { get; private set; } = null!;
    }
}


