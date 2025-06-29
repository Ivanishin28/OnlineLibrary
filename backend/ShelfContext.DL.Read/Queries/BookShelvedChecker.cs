using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class BookShelvedChecker : IBookShelvedChecker
    {
        private ShelfReadDbContext _db;

        public BookShelvedChecker(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsBookShelvedBy(BookId bookId, UserId userId)
        {
            return await _db
                .ShelvedBooks
                .AnyAsync(shelvedBook =>
                    shelvedBook.Shelf.UserId == userId.Value &&
                    shelvedBook.BookId == bookId.Value);
        }
    }
}
