using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Dtos
{
    public record IdentityPreviewDto
    {
        [JsonPropertyName("user_id")]
        public required Guid UserId { get; init; }
        [JsonPropertyName("identity_id")]
        public required Guid IdentityId { get; init; }
        [JsonPropertyName("nickname")]
        public required string Nickname { get; init; }
        [JsonPropertyName("avatar_id")]
        public Guid? AvatarId { get; init; }
    }
}
