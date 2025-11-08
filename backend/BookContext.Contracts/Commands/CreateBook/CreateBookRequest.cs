using System.Text.Json.Serialization;
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
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }
        [JsonPropertyName("file_id")]
        public Guid? FileId { get; init; }
        public string? Description { get; init; }
    }
}
