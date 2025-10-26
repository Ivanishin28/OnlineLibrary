using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateBook
{
    public record CreateBookRequest : IResultRequest<Guid?>
    {
        [JsonPropertyName("title")]
        public required string Title { get; init; }
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
    }
}
