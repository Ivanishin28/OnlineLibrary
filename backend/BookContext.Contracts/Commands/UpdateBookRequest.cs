using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands
{
    public record UpdateBookRequest : IResultRequest
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; init; }
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
        [JsonPropertyName("genre_ids")]
        public ICollection<Guid> GenreIds { get; init; } = new List<Guid>();
        [JsonPropertyName("publishing_date")]
        public required DateOnly PublishingDate { get; init; }
        [JsonPropertyName("cover_id")]
        public Guid? CoverId { get; init; }
        [JsonPropertyName("file_id")]
        public Guid? FileId { get; init; }
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}
