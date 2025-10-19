using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Services;

namespace ShelfContext.DL.Read.Queries
{
    public class ResouceAccessibilityChecker : IResouceAccessibilityChecker
    {
        private ShelfReadDbContext _db;

        public ResouceAccessibilityChecker(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<bool> IsBookAccessibleToUser(Guid bookId, Guid userId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsShelfAccesibleToUser(Guid shelfId, Guid userId)
        {
            return _db
                .Shelves
                .AnyAsync(x =>
                    x.Id == shelfId &&
                    x.UserId == userId);
        }

        public Task<bool> IsShelvedBookAccessibleToUser(Guid shelvedBookId, Guid userId)
        {
            return _db
                .ShelvedBooks
                .AnyAsync(x =>
                    x.Id == shelvedBookId &&
                    x.UserId == userId);
        }

        public Task<bool> IsTagAccessibleToUser(Guid tagId, Guid userId)
        {
            return _db
                .Tags
                .AnyAsync(x =>
                    x.Id == tagId &&
                    x.UserId == userId);
        }
    }
}
