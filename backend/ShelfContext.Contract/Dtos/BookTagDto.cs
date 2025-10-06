using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Dtos
{
    public record BookTagDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; init; }
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; init; }
        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;
    }
}
