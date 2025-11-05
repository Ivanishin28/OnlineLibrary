using System.Text.Json.Serialization;

namespace BookContext.Contract.Dtos
{
    public record GenreDto
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; init; }
        [JsonPropertyName("name")]
        public required string Name { get; init; }
    }
}
