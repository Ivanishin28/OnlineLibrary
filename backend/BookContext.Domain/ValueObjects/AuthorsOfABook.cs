using BookContext.Domain.Entities;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public class AuthorsOfABook
    {
        private Guid BookId { get; set; }
        private ICollection<BookAuthor> _bookAuthors;

        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.ToImmutableList();
        public IReadOnlyCollection<Guid> AuthorIds => _bookAuthors.Select(bookAuthor => bookAuthor.AuthorId).ToList();

        private AuthorsOfABook(Guid bookId, ICollection<BookAuthor> bookAuthors)
        {
            BookId = bookId;
            _bookAuthors = bookAuthors;
        }

        public Result SetAuthors(ICollection<Guid> authorIds)
        {
            if (authorIds is null || authorIds.Count() == 0)
            {
                return Result.Failure("Book should have authors");
            }

            if (HasDuplicates(authorIds))
            {
                return Result.Failure("Book should have unique authors");
            }

            var oldBookAuthors = _bookAuthors
                .Where(bookAuthor => authorIds.Contains(bookAuthor.AuthorId))
                .ToList();

            var toAdd = authorIds
                .Where(authorId => 
                    !oldBookAuthors.Any(bookAuthor 
                        => bookAuthor.AuthorId == authorId));

            return Result.Success();
        }

        private static bool HasDuplicates(ICollection<Guid> authors)
        {
            var distinctAuthors = authors
                .Distinct();

            return distinctAuthors.Count() < authors.Count();
        }

        public static Result<AuthorsOfABook> Create(Guid bookId, ICollection<Guid> authorIds)
        {
            if(HasDuplicates(authorIds))
            {
                return Result<AuthorsOfABook>.Failure("Has duplicates");
            }

            var bookAuthors = authorIds
                .Select(authorId => 
                    BookAuthor.Create(bookId, authorId))
                .ToList();

            return new AuthorsOfABook(bookId, bookAuthors);
        }

        public static Result<AuthorsOfABook> Create(ICollection<BookAuthor> bookAuthors)
        {
            var bookId = bookAuthors.First().BookId;

            if(bookAuthors.Any(bookAuthor => bookAuthor.BookId != bookId))
            {
                return Result<AuthorsOfABook>.Failure("Should be associated with only one book");
            }

            return new AuthorsOfABook(bookId, bookAuthors);
        }
    }
}
