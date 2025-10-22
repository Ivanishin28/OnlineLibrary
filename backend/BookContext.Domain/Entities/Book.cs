using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        public BookId Id { get; private set; } = null!;
        public UserId CreatorId { get; private set; } = null!;
        public string Title { get; private set; } = null!;

        private Book() { }

        private Book(BookId id, string title, UserId creatorId)
        {
            Id = id;
            Title = title;
            CreatorId = creatorId;
        }

        public static Result<Book> Create(UserId creatorId, string title)
        {
            var bookId = new BookId(Guid.NewGuid());

            if(String.IsNullOrEmpty(title))
            {
                return Result<Book>.Failure(BookErrors.EmptyTitle);
            }

            return new Book(bookId, title, creatorId);
        }
    }
}
