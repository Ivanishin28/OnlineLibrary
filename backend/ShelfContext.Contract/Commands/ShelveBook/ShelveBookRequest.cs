using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.ShelveBook
{
    public class ShelveBookRequest : IResultRequest
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
