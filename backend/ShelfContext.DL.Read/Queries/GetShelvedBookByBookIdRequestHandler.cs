using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries.GetShelvedBookByBookId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetShelvedBookByBookIdRequestHandler : IRequestHandler<GetShelvedBookByBookIdRequest, ShelvedBookDto?>
    {
        private ShelfReadDbContext _db;

        public GetShelvedBookByBookIdRequestHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<ShelvedBookDto?> Handle(GetShelvedBookByBookIdRequest request, CancellationToken cancellationToken)
        {
            return await _db
                .ShelvedBooks
                .Where(x =>
                    x.BookId == request.BookId &&
                    x.Shelf.UserId == request.UserId)
                .Select(x => new ShelvedBookDto(x.Id, x.BookId, x.ShelfId, x.DateShelved))
                .FirstOrDefaultAsync();
        }
    }
}
