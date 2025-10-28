using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Entities.Review
{
    public class Review
    {
        public ReviewId Id { get; private set; } = null!;
        public UserId UserId { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public ReviewText Text { get; private set; } = null!;
        public Rating Rating { get; private set; } = null!;
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Review() { }

        private Review(
            ReviewId id, UserId userId, BookId bookId, 
            ReviewText text, Rating rating, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            Text = text;
            Rating = rating;
            CreatedAt = createdAt;
            UpdatedAt = createdAt;
        }

        public static Review Create(
            UserId userId, BookId bookId, 
            Rating rating, ReviewText text)
        {
            var id = new ReviewId(Guid.NewGuid());
            return new Review(id, userId, bookId, text, rating, DateTime.Now);
        }

        public void UpdateRating(Rating rating)
        {
            Rating = rating;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateText(ReviewText text)
        {
            Text = text;
            UpdatedAt = DateTime.Now;
        }
    }
}
