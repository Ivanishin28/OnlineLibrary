using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Extensions;
using Shared.Core.Models;
using System.Collections.Immutable;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private List<BookAuthor> _bookAuthors = null!;

        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;

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
            if(authorsOfABook.BookId != Id)
            {
                Result.Failure(BookErrors.DifferentBookAuthor);
            }

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

            if(String.IsNullOrEmpty(title))
            {
                return Result<Book>.Failure(BookErrors.EmptyTitle);
            }

            if(authorIds.Count() == 0)
            {
                return Result<Book>.Failure(BookErrors.EmptyAuthorList);
            }

            if(authorIds.AllUnique())
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
