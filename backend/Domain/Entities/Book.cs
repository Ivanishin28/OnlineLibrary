using Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; init; }
        public DateOnly PublicationDate { get; init; }
        public ICollection<BookAuthor> BookAuthors { get; init; }

        private Book() { }
        
        private Book(string title, DateOnly publicationDate, ICollection<BookAuthor> bookAuthors)
        {
            Title = title;
            PublicationDate = publicationDate;
            BookAuthors = bookAuthors;
        }

        public static Result<Book> Create(string title, DateOnly publicationDate, ICollection<Author> authors)
        {
            if (String.IsNullOrEmpty(title) || title.Length > 32)
            {
                return Result<Book>.Failure("Invalid title of the book");
            }

            if (authors is null || authors.Count == 0)
            {
                return Result<Book>.Failure("A book can not exist without authors");
            }

            var bookAuthors = authors
                .Select(author => BookAuthor.Create(author.Id).Value)
                .ToList();

            var book = new Book(title, publicationDate, bookAuthors);
            return Result<Book>.Success(book);
        }
    }
}
