using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.ShelveBook
{
    public class ShelveBookResponse
    {
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }

        public ShelveBookResponse(Guid shelvedBookId)
        {
            ShelvedBookId = shelvedBookId;
        }
    }
}
