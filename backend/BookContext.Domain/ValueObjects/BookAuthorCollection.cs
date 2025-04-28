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

        public BooksAuthorCollection(Guid bookId, ICollection<BookAuthor> bookAuthors)
        {
            BookId = bookId;
            _bookAuthors = bookAuthors;

            if(bookAuthors.Any(bookAuthor => bookAuthor.BookId != bookId))
            {
                throw new ArgumentException();
            }
        }

        public Result Add(Guid authorId)
        {
            if (HasAuthor(authorId))
            {
                return Result.Failure("Already has this author");
            }

            var bookAuthor = BookAuthor.Create(BookId, authorId);
            _bookAuthors.Add(bookAuthor);

            return Result.Success();
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

        private bool HasAuthor(Guid authorId)
        {
            return _bookAuthors
                .Any(bookAuthor => bookAuthor.AuthorId == authorId);
        }

        private static bool HasDuplicates(ICollection<Guid> authors)
        {
            var distinctAuthors = authors
                .Distinct();

            return distinctAuthors.Count() < authors.Count();
        }
    }
}
