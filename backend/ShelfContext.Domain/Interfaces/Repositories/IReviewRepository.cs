using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public void Add(Review review);
        public void Remove(Review review);

        public Task<Review?> GetBy(ReviewId id);
        public Task<Review?> GetBy(UserId userId, BookId bookId);
        public Task<ICollection<Review>> GetAllBy(BookId bookId);

        public Task<bool> Exists(UserId userId, BookId bookId);
    }
}
