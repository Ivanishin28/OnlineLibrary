using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser;

namespace ShelfContext.DL.Read.Queries
{
    public class IsShelfNameTakenByUserQueryHandler : IRequestHandler<IsShelfNameTakenByUserQuery, bool>
    {
        private ShelfReadDbContext _db;

        public IsShelfNameTakenByUserQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsShelfNameTakenByUserQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .AnyAsync(shelf => 
                    shelf.Name == request.ShelfName.Value &&
                    shelf.UserId == request.UserId.Value);
        }
    }
}
