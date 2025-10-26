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
        public required Guid CreatorId { get; init; }
        public required string Title { get; init; }
        public ICollection<Guid> AuthorIds { get; init; } = new List<Guid>();
        public DateOnly PublishingDate { get; init; }
        public Guid? CoverId { get; init; }
        public string? Description { get; init; }
    }
}
