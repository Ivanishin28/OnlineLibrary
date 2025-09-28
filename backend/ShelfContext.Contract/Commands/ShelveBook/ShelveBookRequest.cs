using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Commands.ShelveBook
{
    public class ShelveBookRequest : IResultRequest<Guid?>
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; private set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }

        public ShelveBookRequest(Guid bookId, Guid shelfId, Guid userId)
        {
            BookId = bookId;
            ShelfId = shelfId;
            UserId = userId;
        }
    }
}
