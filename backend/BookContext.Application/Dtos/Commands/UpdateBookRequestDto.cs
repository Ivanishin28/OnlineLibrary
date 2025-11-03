using System.Text.Json.Serialization;

namespace BookContext.Application.Dtos.Commands
{
    public record UpdateBookRequestDto
    {
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
        
        [JsonPropertyName("publishing_date")]
        public DateOnly PublishingDate { get; init; }
        
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }
        
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}

