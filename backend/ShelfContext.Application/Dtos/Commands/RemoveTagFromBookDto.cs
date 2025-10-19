using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Application.Dtos.Commands
{
    public record RemoveTagFromBookDto
    {
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }
        [JsonPropertyName("tag_id")]
        public Guid TagId { get; private set; }

        public RemoveTagFromBookDto(Guid shelvedBookId, Guid tagId)
        {
            ShelvedBookId = shelvedBookId;
            TagId = tagId;
        }
    }
}
