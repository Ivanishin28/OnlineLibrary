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
        private readonly List<BookAuthor> _bookAuthors = new();

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

        private Book() { }

        private Book(Guid id, string title, ICollection<BookAuthor> bookAuthors)
        {
            Id = id;
            Title = title;
            _bookAuthors.AddRange(bookAuthors);
        }

        public Result<Book> Create(string title, ICollection<Author> authors)
        {
            if(authors is null || authors.Count() == 0)
            {
                return Result<Book>.Failure("Book should have authors");
            }

            if(HasDuplicates(authors))
            {
                return Result<Book>.Failure("Book should have unique authors");
            }

            var bookId = Guid.NewGuid();

            var bookAuthors = authors
                .Select(author =>
                    BookAuthor.Create(bookId, author.Id))
                .ToList();

            return new Book(bookId, title, bookAuthors);
        }

        private static bool HasDuplicates(ICollection<Author> authors)
        {
            var distinctAuthors = authors
                .Select(author => author.Id)
                .Distinct();

            return distinctAuthors.Count() < authors.Count();
        }
    }
}
