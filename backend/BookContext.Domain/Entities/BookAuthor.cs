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
        public Guid BookId { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private BookAuthor() { }

        private BookAuthor(Guid bookId, Guid authorId, DateTime createdAt)
        {
            BookId = bookId;
            AuthorId = authorId;
            CreatedAt = createdAt;
        }

        public static BookAuthor Create(Guid bookId, Guid authorId)
        {
            return new BookAuthor(bookId, authorId, DateTime.Now);
        }
    }
}
