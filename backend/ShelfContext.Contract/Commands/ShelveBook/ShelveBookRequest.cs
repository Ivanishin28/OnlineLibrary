using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Commands.ShelveBook
{
    public class ShelveBookRequest : IResultRequest<ShelveBookResponse>
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; private set; }

        public ShelveBookRequest(Guid bookId, Guid shelfId)
        {
            BookId = bookId;
            ShelfId = shelfId;
        }
    }
}
