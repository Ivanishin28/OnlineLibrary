using BookContext.Domain.ValueObjects;
using Shared.Core.Models;
using System.Collections.Immutable;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private ICollection<BookAuthor> _bookAuthors;

        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.ToImmutableArray();

        private Book() { }

        private Book(Guid id, string title, ICollection<BookAuthor> bookAuthors)
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

            var bookAuthors = authorIds
                .Select(authorId => 
                    BookAuthor.Create(bookId, authorId))
                .ToList();

            return new Book(bookId, title, bookAuthors);
        }
    }
}
