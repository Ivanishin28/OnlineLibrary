using System.Text.Json.Serialization;

namespace ShelfContext.Application.Dtos.Commands
{
    public class CreateTagRequestDto
    {
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public CreateTagRequestDto(string name)
        {
            Name = name;
        }
    }
}
