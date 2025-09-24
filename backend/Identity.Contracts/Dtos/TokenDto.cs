using System.Text.Json.Serialization;

namespace IdentityContext.Contracts.Dtos
{
    public record TokenDto
    {
        [JsonPropertyName("value")]
        public string Value { get; private set; }

        public TokenDto(string value)
        {
            Value = value;
        }
    }
}
