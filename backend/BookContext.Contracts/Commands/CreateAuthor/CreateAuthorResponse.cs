using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public class CreateAuthorResponse
    {
        [JsonPropertyName("author_id")]
        public Guid AuthorId { get; private set; }

        public CreateAuthorResponse(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}
