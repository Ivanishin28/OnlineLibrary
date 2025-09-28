using Microsoft.EntityFrameworkCore;
using Shared.Core.Interfaces;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Enums;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class BookAccessor : IBookAccessor
    {
        private ShelfDbContext _db;
        private IUserContext _userContext;

        public BookAccessor(ShelfDbContext db, IUserContext userContext)
        {
            _db = db;
            _userContext = userContext;
        }

        public Task<Book?> GetBy(BookId id)
        {
            var userId = new UserId(_userContext.UserId);

            return _db
                .Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => 
                    x.Id == id &&
                    (x.Visibility == BookVisibility.Public ||
                    x.CreatorId == userId));
        }
    }
}
