using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Queries.GetLibrarySummary
{
    public record LibrarySummary(
        [property: JsonPropertyName("user_id")] Guid UserId,
        [property: JsonPropertyName("shelves")] ICollection<ShelfSummary> Shelves,
        [property: JsonPropertyName("tags")] ICollection<TagSummary> Tags)
    {
    }
}
