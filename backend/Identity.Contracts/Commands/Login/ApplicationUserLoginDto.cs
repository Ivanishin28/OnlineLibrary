using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Commands.Login
{
    public record ApplicationUserLoginDto
    {
        [JsonPropertyName("email")]
        public string Email { get; private set; }
        [JsonPropertyName("login")]
        public string Login { get; private set; }
        [JsonPropertyName("user_id")]
        public Guid ApplicationUserId { get; private set; }
        [JsonPropertyName("is_registration_complete")]
        public bool IsRegistrationComplete { get; private set; }
        public ApplicationUserLoginDto(string email, string login, Guid applicationUserId, bool isRegistrationComplete)
        {
            Email = email;
            Login = login;
            ApplicationUserId = applicationUserId;
            IsRegistrationComplete = isRegistrationComplete;
        }
    }
}
