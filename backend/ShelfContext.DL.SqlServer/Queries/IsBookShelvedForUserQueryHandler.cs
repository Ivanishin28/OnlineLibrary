using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Queries
{
    public class IsBookShelvedForUserQueryHandler : IRequestHandler<IsBookShelvedForUserQuery, bool>
    {
        private ShelfDbContext _db;

        public IsBookShelvedForUserQueryHandler(ShelfDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(IsBookShelvedForUserQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .Join(
                    _db.ShelvedBooks,
                    shelf => shelf.Id,
                    shelvedBook => shelvedBook.ShelfId,
                    (shelf, shelvedBook) => new
                    {
                        UserId = shelf.UserId,
                        BookId = shelvedBook.BookId
                    }
                )
                .AnyAsync(userBook => 
                    request.UserId == userBook.UserId &&
                    request.BookId == userBook.BookId
                );
        }
    }
}
