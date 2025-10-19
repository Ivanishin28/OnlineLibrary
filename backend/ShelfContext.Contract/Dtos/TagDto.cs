using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Dtos
{
    public record TagDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;
    }
}
