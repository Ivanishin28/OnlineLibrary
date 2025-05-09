using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.CreateUser
{
    public class CreateUserResponse
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }

        public CreateUserResponse(Guid userId)
        {
            UserId = userId;
        }
    }
}
