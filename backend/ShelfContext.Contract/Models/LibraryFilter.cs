using System.Text.Json.Serialization;

namespace Composition.Contract.Models
{
    public record LibraryFilter
    {
        [JsonPropertyName("tag_id")]
        public Guid? TagId { get; init; }
        [JsonPropertyName("shelf_id")]
        public Guid? ShelfId { get; init; }
    }
}
