using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetBookShelvedCountRequestHandler : IRequestHandler<GetBookShelvedCountQuery, int>
    {
        private ShelfReadDbContext _db;

        public GetBookShelvedCountRequestHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<int> Handle(GetBookShelvedCountQuery request, CancellationToken cancellationToken)
        {
            return _db
                .ShelvedBooks
                .Where(x => x.BookId == request.BookId)
                .CountAsync();
        }
    }
}
