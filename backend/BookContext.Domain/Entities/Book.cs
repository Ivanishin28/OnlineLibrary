using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private AuthorsOfABook _bookAuthors;

        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public AuthorsOfABook AuthorsOfABook => _bookAuthors;

        private Book() { }

        private Book(Guid id, string title, AuthorsOfABook bookAuthors)
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

        public static Result<Book> Create(string title, ICollection<Guid> authorIds)
        {
            var bookId = Guid.NewGuid();

            var collectionResult = AuthorsOfABook.Create(bookId, authorIds);

            if(collectionResult.IsFailure)
            {
                return collectionResult.ToFailure<Book>();
            }

            return new Book(bookId, title, collectionResult.Model);
        }
    }
}
