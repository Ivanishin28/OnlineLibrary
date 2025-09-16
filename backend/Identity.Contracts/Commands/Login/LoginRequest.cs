using MediatR;
using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Commands.Login
{
    public record LoginRequest : IResultRequest<ApplicationUserLoginDto>
    {
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        [JsonPropertyName("password")]
        public string Password { get; private set; }

        public LoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
