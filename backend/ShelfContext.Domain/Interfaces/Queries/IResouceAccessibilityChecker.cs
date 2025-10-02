using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Queries
{
    public interface IResouceAccessibilityChecker
    {
        Task<bool> IsAccessible(BookId bookId, UserId userId);
        Task<bool> IsAccessible(ShelfId shelfId, UserId userId);
        Task<bool> IsAccessible(TagId tagId, UserId userId);
    }
}
