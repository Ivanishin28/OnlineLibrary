using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Interfaces.Queries.IsNameUniqueForUser;

namespace ShelfContext.DL.SqlServer.Queries
{
    public class IsNameUniqueForUserQueryHandler : IRequestHandler<IsNameUniqueForUserQuery, bool>
    {
        private ShelfDbContext _db;

        public IsNameUniqueForUserQueryHandler(ShelfDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsNameUniqueForUserQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .AnyAsync(shelf =>
                    shelf.Name == request.ShelfName &&
                    shelf.UserId == request.UserId);
        }
    }
}
