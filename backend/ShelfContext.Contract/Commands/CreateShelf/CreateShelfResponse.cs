using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Commands.CreateShelf
{
    public class CreateShelfResponse
    {
        [JsonPropertyName("created_shelf_id")]
        public Guid CreatedShelfId { get; private set; }

        public CreateShelfResponse(Guid createdShelfId)
        {
            CreatedShelfId = createdShelfId;
        }
    }
}
