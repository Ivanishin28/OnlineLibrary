using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class IsBookShelvedForUserQueryHandler : IRequestHandler<IsBookShelvedForUserQuery, bool>
    {
        private ShelfReadDbContext _db;

        public IsBookShelvedForUserQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsBookShelvedForUserQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .ShelvedBooks
                .AnyAsync(shelvedBook =>
                    shelvedBook.Shelf.UserId == request.UserId.Value &&
                    shelvedBook.BookId == request.BookId.Value);
        }
    }
}
