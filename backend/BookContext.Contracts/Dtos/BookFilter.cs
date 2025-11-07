using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Dtos
{
    public record BookFilter
    {
        [JsonPropertyName("genre_ids")]
        public required ICollection<Guid> GenreIds { get; init; }
    }
}
