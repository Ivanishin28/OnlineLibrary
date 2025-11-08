using System.Text.Json.Serialization;

namespace BookContext.Contract.Dtos
{
    public record FullBookDto
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; init; }
        
        [JsonPropertyName("title")]
        public required string Title { get; init; }
        
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }

        [JsonPropertyName("file_id")]
        public Guid? FileId { get; init; }

        [JsonPropertyName("authors")]
        public IReadOnlyCollection<AuthorDto> Authors { get; init; } = new List<AuthorDto>();
        
        [JsonPropertyName("publishing_date")]
        public required DateOnly PublishingDate { get; init; }
        
        [JsonPropertyName("description")]
        public string? Description { get; init; }
        [JsonPropertyName("genres")]
        public required ICollection<GenreDto> Genres { get; init; } = new List<GenreDto>();
    }
}
