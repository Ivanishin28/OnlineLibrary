using System.Text.Json.Serialization;

namespace BookContext.Contract.Dtos
{
    public record FullBookDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("title")]
        public string Title { get; init; }
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }
        [JsonPropertyName("authors")]
        public IReadOnlyCollection<AuthorDto> Authors { get; init; }

        public FullBookDto(Guid id, string title, Guid? coverId, IReadOnlyCollection<AuthorDto> authors)
        {
            Id = id;
            Title = title;
            CoverId = coverId;
            Authors = authors;
        }
    }
}
