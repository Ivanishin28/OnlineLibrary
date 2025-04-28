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

        private Book(Guid id, string title, BooksAuthorCollection bookAuthors)
        {
            Id = id;
            Title = title;
            _bookAuthors = bookAuthors;
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
            return _bookAuthors.SetAuthors(authorIds);
        }

        public static Result<Book> Create(string title, ICollection<Guid> authorIds)
        {
            var bookId = Guid.NewGuid();

            var collectionResult = BooksAuthorCollection.Create(bookId, authorIds);

            if(collectionResult.IsFailure)
            {
                return collectionResult.ToFailute<Book>();
            }

            return new Book(bookId, title, collectionResult.Model);
        }
    }
}
