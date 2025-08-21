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
        private List<BookAuthor> _bookAuthors;

        public Guid BookId { get; private set; }
        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.ToImmutableList();

        private AuthorsOfABook(Guid bookId, List<BookAuthor> bookAuthors)
        {
            BookId = bookId;
            _bookAuthors = bookAuthors;
        }

        public void Add(Guid authorId)
        {
            if(!HasAuthor(authorId))
            {
                return;
            }

            _bookAuthors.Add(BookAuthor.Create(BookId, authorId));
        }

        public void Remove(Guid authorId)
        {
            if (!HasAuthor(authorId))
            {
                return;
            }

            var bookAuthor = _bookAuthors
                .First(bookAuthor => bookAuthor.Id == authorId);
            _bookAuthors.Remove(bookAuthor);
        }

        public void SetAuthors(ICollection<Guid> authors)
        {
            var remainingOldAuthors = _bookAuthors
                .Where(bookAuthor => authors.Contains(bookAuthor.Id))
                .ToList();

            var newAuthors = authors
                .Where(authorId => 
                    !remainingOldAuthors.Any(remainingAuthor => remainingAuthor.Id == authorId))
                .Select(newAuthorId => BookAuthor.Create(BookId, newAuthorId));

            _bookAuthors.Clear();
            _bookAuthors.AddRange(remainingOldAuthors);
            _bookAuthors.AddRange(newAuthors);
        }

        public bool HasAuthor(Guid authorId)
        {
            return _bookAuthors.Any(bookAuthor => bookAuthor.AuthorId == authorId);
        }

        public static AuthorsOfABook CreateFrom(Book book)
        {
            return new AuthorsOfABook(book.Id, book.BookAuthors.ToList());
        }
    }
}
