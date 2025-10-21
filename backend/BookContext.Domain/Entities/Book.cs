using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public UserId CreatorId { get; private set; } = null!;
        public string Title { get; private set; } = null!;

        private Book() { }

        private Book(Guid id, string title, UserId creatorId)
        {
            Id = id;
            Title = title;
            CreatorId = creatorId;
        }

        public static Result<Book> Create(UserId creatorId, string title)
        {
            var bookId = Guid.NewGuid();

            if(String.IsNullOrEmpty(title))
            {
                return Result<Book>.Failure(BookErrors.EmptyTitle);
            }

            return new Book(bookId, title, creatorId);
        }
    }
}
