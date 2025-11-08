using System.Text.Json.Serialization;

namespace ShelfContext.Application.Dtos.Commands
{
    public record AddBookReviewRequestDto
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; init; }

        [JsonPropertyName("rating")]
        public int Rating { get; init; }

        [JsonPropertyName("text")]
        public string? Text { get; init; }
    }
}

