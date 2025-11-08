using Shared.Contracts.Interfaces;

namespace ShelfContext.Contract.Commands
{
    public record AddBookReviewRequest : IResultRequest<Guid?>
    {
        public required Guid UserId { get; init; }
        public required Guid BookId { get; init; }
        public required int Rating { get; init; }
        public string? Text { get; init; }
    }
}
