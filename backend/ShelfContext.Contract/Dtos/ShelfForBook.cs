using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Dtos
{
    public record ShelfForBook()
    {
        [JsonPropertyName("shelf_name")]
        public required string ShelfName { get; init; }
        [JsonPropertyName("count")]
        public required int Count { get; init; }
    }
}
