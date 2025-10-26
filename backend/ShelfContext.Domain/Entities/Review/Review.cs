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
        public ShelvedBookId ShelvedBookId { get; private set; } = null!;
        public ReviewText Text { get; private set; } = null!;
        public Rating Rating { get; private set; } = null!;
        public DateTime CreatedAt { get; set; }

        private Review() { }

        public Review(
            UserId userId, BookId bookId, ShelvedBookId shelvedBookId, 
            ReviewText text, Rating rating, DateTime createdAt)
        {
            Id = new ReviewId(Guid.NewGuid());
            UserId = userId;
            BookId = bookId;
            ShelvedBookId = shelvedBookId;
            Text = text;
            Rating = rating;
            CreatedAt = createdAt;
        }
    }
}
