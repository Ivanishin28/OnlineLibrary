using IdentityContext.Contracts.Dtos;
using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Commands.Login
{
    public record ApplicationUserLoginDto
    {
        [JsonPropertyName("email")]
        public string Email { get; private set; }
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        [JsonPropertyName("identity_id")]
        public Guid ApplicationUserId { get; private set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }
        [JsonPropertyName("token")]
        public TokenDto Token { get; private set; }

        public ApplicationUserLoginDto(string email, string login, Guid applicationUserId, Guid userId, TokenDto token)
        {
            Email = email;
            Login = login;
            ApplicationUserId = applicationUserId;
            UserId = userId;
            Token = token;
        }
    }
}
