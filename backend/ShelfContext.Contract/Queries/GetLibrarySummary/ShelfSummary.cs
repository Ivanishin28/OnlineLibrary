using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Queries.GetLibrarySummary
{
    public record ShelfSummary(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("book_count")] int BookCount)
    {
    }
}
