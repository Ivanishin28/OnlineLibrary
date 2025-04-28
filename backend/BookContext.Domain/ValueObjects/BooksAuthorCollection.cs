using BookContext.Domain.Entities;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public class BooksAuthorCollection
    {
        private Guid BookId { get; set; }
        private ICollection<BookAuthor> _bookAuthors;

        private BooksAuthorCollection(Guid bookId, ICollection<BookAuthor> bookAuthors)
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

        public static Result<BooksAuthorCollection> Create(Guid bookId, ICollection<Guid> authorIds)
        {
            if(HasDuplicates(authorIds))
            {
                return Result<BooksAuthorCollection>.Failure("Has duplicates");
            }

            var bookAuthors = authorIds
                .Select(authorId => 
                    BookAuthor.Create(bookId, authorId))
                .ToList();

            return new BooksAuthorCollection(bookId, bookAuthors);
        }

        public static Result<BooksAuthorCollection> Create(ICollection<BookAuthor> bookAuthors)
        {
            var bookId = bookAuthors.First().BookId;

            if(bookAuthors.Any(bookAuthor => bookAuthor.BookId != bookId))
            {
                return Result<BooksAuthorCollection>.Failure("Should be assosiated with only one book");
            }

            return new BooksAuthorCollection(bookId, bookAuthors);
        }
    }
}
