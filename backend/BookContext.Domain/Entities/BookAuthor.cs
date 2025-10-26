using BookContext.Domain.ValueObjects;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Entities
{
    public class BookAuthor
    {
        public BookAuthorId Id { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public AuthorId AuthorId { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private BookAuthor() { }

        public BookAuthor(BookAuthorId id, BookId bookId, AuthorId authorId, DateTime createdAt)
        {
            Id = id;
            BookId = bookId;
            AuthorId = authorId;
            CreatedAt = createdAt;
        }
    }
}
