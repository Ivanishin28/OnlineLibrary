using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.AddTagToBook
{
    public class AddTagToBookRequest : IResultRequest<AddTagToBookResponse>
    {
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; private set; }

        public AddTagToBookRequest(Guid shelvedBookId, Guid tagId)
        {
            ShelvedBookId = shelvedBookId;
            TagId = tagId;
        }
    }
}
