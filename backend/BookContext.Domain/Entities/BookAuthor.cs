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
        public Guid Id { get; private set; }
        public BookId BookId { get; private set; } = null!;
        public AuthorId AuthorId { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private BookAuthor() { }

        private BookAuthor(BookId bookId, AuthorId authorId, DateTime createdAt)
        {
            BookId = bookId;
            AuthorId = authorId;
            CreatedAt = createdAt;
        }

        public static BookAuthor Create(BookId bookId, AuthorId authorId)
        {
            return new BookAuthor(bookId, authorId, DateTime.Now);
        }
    }
}
