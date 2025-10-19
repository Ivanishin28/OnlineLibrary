using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Queries
{
    public class TagNameUniquenessChecker : ITagNameUniquenessChecker
    {
        private readonly ShelfReadDbContext _db;

        public TagNameUniquenessChecker(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<bool> IsNameTakenBy(TagName name, UserId userId)
        {
            return _db.Tags.AnyAsync(x =>
                x.UserId == userId.Value &&
                x.Name == name.Value);
        }
    }
}
