using BookContext.Domain.ValueObjects;
using System;

namespace BookContext.Domain.Entities
{
    public class BookGenre
    {
        public BookGenreId Id { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public GenreId GenreId { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private BookGenre() { }

        public BookGenre(BookGenreId id, BookId bookId, GenreId genreId, DateTime createdAt)
        {
            Id = id;
            BookId = bookId;
            GenreId = genreId;
            CreatedAt = createdAt;
        }
    }
}

