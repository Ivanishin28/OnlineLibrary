using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Commands.Register
{
    public record RegisterRequest : IResultRequest<Guid>
    {
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        [JsonPropertyName("email")]
        public string Email { get; private set; }
        [JsonPropertyName("password")]
        public string Password { get; private set; }

        public RegisterRequest(string login, string emial, string password)
        {
            Email = emial;
            Password = password;
            Login = login;
        }
    }
}
