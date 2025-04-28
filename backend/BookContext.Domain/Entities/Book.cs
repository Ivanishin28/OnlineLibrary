using BookContext.Domain.ValueObjects;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private BooksAuthorCollection _bookAuthors;

        public Guid Id { get; private set; }
        public string Title { get; private set; }

        private Book() { }

        private Book(Guid id, string title, ICollection<BookAuthor> bookAuthors)
        {
            Id = id;
            Title = title;
            _bookAuthors = new BooksAuthorCollection(id, bookAuthors);
        }

        public Result UpdateTitle(string title)
        {
            if(String.Equals(Title, title))
            {
                return Result.Success();
            }

            Title = title;

            return Result.Success();
        }

        public Result SetAuthors(ICollection<Guid> authorIds)
        {


            return Result.Success();
        }

        public static Result<Book> Create(string title, ICollection<Guid> authorIds)
        {
            var bookId = Guid.NewGuid();

            var bookAuthors = new BooksAuthorCollection(bookId, )

            return new Book(bookId, title, bookAuthors);
        }
    }
}
