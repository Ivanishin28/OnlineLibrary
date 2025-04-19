using Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookAuthor : Entity
    {
        public Guid AuthorId { get; init; }
        public Guid BookId { get; init; }

        private BookAuthor() { }

        private BookAuthor(Guid authorId)
        {
            AuthorId = authorId;
        }

        public static Result<BookAuthor> Create(Guid authorId)
        {
            var bookAuthor = new BookAuthor(authorId);
            return Result<BookAuthor>.Success(bookAuthor);
        }
    }
}
