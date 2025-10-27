using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private ShelfDbContext _db;

        public ReviewRepository(ShelfDbContext db)
        {
            _db = db;
        }

        public void Add(Review review)
        {
            _db.Add(review);
        }

        public void Remove(Review review)
        {
            _db.Remove(review);
        }

        public Task<Review?> GetBy(ReviewId id)
        {
            return _db.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Review?> GetBy(UserId userId, BookId bookId)
        {
            return _db
                .Reviews
                .FirstOrDefaultAsync(x => 
                    x.UserId == userId && 
                    x.BookId == bookId);
        }

        public Task<bool> Exists(UserId userId, BookId bookId)
        {
            return _db
                .Reviews
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.BookId == bookId);
        }
    }
}
