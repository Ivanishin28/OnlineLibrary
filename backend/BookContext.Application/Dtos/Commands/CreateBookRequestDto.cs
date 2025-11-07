using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Application.Dtos.Commands
{
    public record CreateBookRequestDto
    {
        [JsonPropertyName("title")]
        public string Title { get; init; } = null!;
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
        [JsonPropertyName("genre_ids")]
        public ICollection<Guid> GenreIds { get; init; } = new List<Guid>();
        [JsonPropertyName("publishing_date")]
        public DateOnly PublishingDate { get; init; }
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}
