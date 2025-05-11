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
        [JsonPropertyName("book_on_a_shelf_id")]
        public Guid BookOnAShelfId { get; private set; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; private set; }

        public AddTagToBookRequest(Guid bookOnAShelfId, Guid tagId)
        {
            BookOnAShelfId = bookOnAShelfId;
            TagId = tagId;
        }
    }
}
