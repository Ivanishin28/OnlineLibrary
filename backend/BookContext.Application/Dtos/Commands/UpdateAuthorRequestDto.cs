using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Application.Dtos.Commands
{
    public record UpdateAuthorRequestDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("biography")]
        public string? Biography { get; init; }

        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; init; }

        [JsonPropertyName("avatar_id")]
        public Guid? AvatarId { get; init; }
    }
}
