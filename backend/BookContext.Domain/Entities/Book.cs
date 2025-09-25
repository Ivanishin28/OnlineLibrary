using BookContext.Domain.Enums;
using BookContext.Domain.Errors;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public Guid CreatorId { get; private set; }
        public BookVisibility Visibility { get; private set; }

        private Book() { }

        private Book(Guid id, string title, Guid creatorId, BookVisibility visibility)
        {
            Id = id;
            Title = title;
            CreatorId = creatorId;
            Visibility = visibility;
        }

        public static Result<Book> Create(Guid creatorId, string title, BookVisibility visibility)
        {
            var bookId = Guid.NewGuid();

            if(String.IsNullOrEmpty(title))
            {
                return Result<Book>.Failure(BookErrors.EmptyTitle);
            }

            return new Book(bookId, title, creatorId, visibility);
        }
    }
}
