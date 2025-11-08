using Shared.Contracts.Interfaces;
using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Commands.Register
{
    public record RegisterRequest : IResultRequest<Guid?>
    {
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        [JsonPropertyName("email")]
        public string Email { get; private set; }
        [JsonPropertyName("password")]
        public string Password { get; private set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; private set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; private set; }
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; private set; }
        [JsonPropertyName("avatar_id")]
        public Guid? AvatarId { get; private set; }

        public RegisterRequest(
            string login,
            string email,
            string password,
            string firstName,
            string lastName,
            DateOnly birthDate,
            Guid? avatarId)
        {
            Email = email;
            Password = password;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            AvatarId = avatarId;
        }
    }
}
