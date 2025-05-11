using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.CreateTag
{
    public class CreateTagResponse
    {
        [JsonPropertyName("created_tag_id")]
        public Guid CreatedTagId { get; private set; }

        public CreateTagResponse(Guid createdTagId)
        {
            CreatedTagId = createdTagId;
        }
    }
}
