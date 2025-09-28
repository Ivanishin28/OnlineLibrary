using System.Text.Json.Serialization;

namespace ShelfContext.Application.Dtos.Commands
{
    public record ShelveBookDto
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; init; }
        
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; init; }
    }
}
