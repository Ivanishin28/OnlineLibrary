using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Queries
{
    public class ShelfNameUniquenessChecker : IShelfNameUniquenessChecker
    {
        private ShelfReadDbContext _db;

        public ShelfNameUniquenessChecker(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsNameTakenBy(ShelfName name, UserId userId)
        {
            return await _db
                .Shelves
                .AnyAsync(shelf =>
                    shelf.Name == name.Value &&
                    shelf.UserId == userId.Value);
        }
    }
}
