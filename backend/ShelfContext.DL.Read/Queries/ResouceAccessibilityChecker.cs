using Microsoft.EntityFrameworkCore;
using Shared.Core.Interfaces;
using ShelfContext.DL.Read.Enums;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Queries
{
    public class ResouceAccessibilityChecker : IResouceAccessibilityChecker
    {
        private ShelfReadDbContext _db;

        public ResouceAccessibilityChecker(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<bool> IsAccessible(BookId bookId, UserId userId)
        {
            return _db
                .Books
                .AnyAsync(x => 
                    x.Id == bookId.Value ||
                    (x.CreatorId == userId.Value &&
                    x.Visibility == BookVisibility.Public));
        }

        public Task<bool> IsAccessible(ShelfId shelfId, UserId userId)
        {
            return _db
                .Shelves
                .AnyAsync(x => 
                    x.Id == shelfId.Value && 
                    x.UserId == userId.Value);
        }

        public Task<bool> IsAccessible(TagId tagId, UserId userId)
        {
            return _db
                .Tags
                .AnyAsync(x =>
                    x.Id == tagId.Value &&
                    x.UserId == userId.Value);
        }
    }
}
