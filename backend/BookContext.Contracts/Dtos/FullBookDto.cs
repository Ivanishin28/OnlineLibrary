using System.Text.Json.Serialization;

namespace BookContext.Contract.Dtos
{
    public record FullBookDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("title")]
        public string Title { get; init; }
        [JsonPropertyName("authors")]
        public IReadOnlyCollection<AuthorDto> Authors { get; init; }

        public FullBookDto(Guid id, string title, IReadOnlyCollection<AuthorDto> authors)
        {
            Id = id;
            Title = title;
            Authors = authors;
        }
    }
}
