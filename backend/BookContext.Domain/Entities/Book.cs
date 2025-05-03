using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Extensions;
using Shared.Core.Models;
using System.Collections.Immutable;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private List<BookAuthor> _bookAuthors;

        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.ToImmutableArray();

        private Book() { }

        private Book(Guid id, string title, ICollection<BookAuthor> bookAuthors)
        {
            Id = id;
            Title = title;
            _bookAuthors = bookAuthors.ToList();
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public Result UpdateAuthors(AuthorsOfABook authorsOfABook)
        {
            if(authorsOfABook.BookAuthors.Count() == 0)
            {
                Result.Failure(BookErrors.EmptyAuthorList);
            }

            _bookAuthors.Clear();
            _bookAuthors.AddRange(authorsOfABook.BookAuthors);

            return Result.Success();
        }

        public static Result<Book> Create(string title, ICollection<Guid> authorIds)
        {
            var bookId = Guid.NewGuid();

            if(authorIds.IsUnique())
            {
                return Result<Book>.Failure(BookErrors.DuplicateAuthors);
            }

            var bookAuthors = authorIds
                .Select(authorId =>
                    BookAuthor.Create(bookId, authorId))
                .ToList();

            return new Book(bookId, title, bookAuthors);
        }
    }
}
