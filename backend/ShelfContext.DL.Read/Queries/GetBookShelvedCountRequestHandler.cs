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
    public class GetBookShelvedCountRequestHandler : IRequestHandler<GetBookShelvedCountQuery, BookShelvedCount>
    {
        private ShelfReadDbContext _db;

        public GetBookShelvedCountRequestHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<BookShelvedCount> Handle(GetBookShelvedCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _db
                .ShelvedBooks
                .Where(x => x.BookId == request.BookId)
                .CountAsync();
            return new BookShelvedCount(count);
        }
    }
}
