using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Dtos
{
    public record SetAvatarRequestDto([property: JsonPropertyName("avatar_id")] Guid AvatarId)
    {
    }
}
