using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Interfaces.Queries.IsNameUniqueForUser;

namespace ShelfContext.DL.Read.Queries
{
    public class IsNameUniqueForUserQueryHandler : IRequestHandler<IsNameUniqueForUserQuery, bool>
    {
        private ShelfReadDbContext _db;

        public IsNameUniqueForUserQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsNameUniqueForUserQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .AnyAsync(shelf => shelf.Name == request.ShelfName.Value);
        }
    }
}
