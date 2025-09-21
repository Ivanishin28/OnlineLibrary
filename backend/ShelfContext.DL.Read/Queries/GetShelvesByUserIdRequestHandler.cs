using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries.GetShelvesByUserId;

namespace ShelfContext.DL.Read.Queries
{
    public class GetShelvesByUserIdRequestHandler : IRequestHandler<GetShelvesByUserIdRequest, IEnumerable<ShelfDto>>
    {
        private ShelfReadDbContext _db;

        public GetShelvesByUserIdRequestHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ShelfDto>> Handle(GetShelvesByUserIdRequest request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .Where(x => x.UserId == request.UserId)
                .Select(x => new ShelfDto(x.Id, x.Name))
                .ToListAsync();
        }
    }
}
