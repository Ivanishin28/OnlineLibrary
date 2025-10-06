using System.Text.Json.Serialization;

namespace ShelfContext.Application.Dtos.Commands
{
    public record AddTagToBookDto
    {
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; private set; }

        public AddTagToBookDto(Guid shelvedBookId, Guid tagId)
        {
            ShelvedBookId = shelvedBookId;
            TagId = tagId;
        }
    }
}
