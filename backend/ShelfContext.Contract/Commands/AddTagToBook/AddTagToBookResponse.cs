using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.AddTagToBook
{
    public class AddTagToBookResponse
    {
        [JsonPropertyName("created_book_tag_id")]
        public Guid CreatedBookTagId { get; private set; }

        public AddTagToBookResponse(Guid createdBookTagId)
        {
            CreatedBookTagId = createdBookTagId;
        }
    }
}
