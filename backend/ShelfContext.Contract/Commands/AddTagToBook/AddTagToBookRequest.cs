using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Commands.AddTagToBook
{
    public class AddTagToBookRequest : IResultRequest<Guid?>
    {
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; private set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }

        public AddTagToBookRequest(Guid shelvedBookId, Guid tagId, Guid userId)
        {
            ShelvedBookId = shelvedBookId;
            TagId = tagId;
            UserId = userId;
        }
    }
}
