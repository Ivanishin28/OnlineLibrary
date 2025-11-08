using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Dtos
{
    public record ReviewDto
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; init; }
        [JsonPropertyName("user_id")]
        public required Guid UserId { get; init; }
        [JsonPropertyName("book_id")]
        public required Guid BookId { get; init; }
        [JsonPropertyName("rating")]
        public required int Rating { get; init; }
        [JsonPropertyName("text")]
        public required string? Text { get; init; }
    }
}
