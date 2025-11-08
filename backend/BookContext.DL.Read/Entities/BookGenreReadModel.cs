using System;

namespace BookContext.DL.Read.Entities
{
    public class BookGenreReadModel
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public Guid GenreId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public BookReadModel Book { get; private set; } = null!;
        public GenreReadModel Genre { get; private set; } = null!;
    }
}

