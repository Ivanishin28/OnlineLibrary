using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Queries.GetLibrarySummary
{
    public record TagSummary(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("book_count")] int BookCount)
    {
    }
}
