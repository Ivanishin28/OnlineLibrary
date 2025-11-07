using Shared.Contracts.Interfaces;

namespace BookContext.Contract.Commands.CreateBook
{
    public record CreateBookRequest : IResultRequest<Guid?>
    {
        public required Guid CreatorId { get; init; }
        public required string Title { get; init; }
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
        public ICollection<Guid> GenreIds { get; init; } = new List<Guid>();
        public DateOnly PublishingDate { get; init; }
        public Guid? CoverId { get; init; }
        public string? Description { get; init; }
    }
}
