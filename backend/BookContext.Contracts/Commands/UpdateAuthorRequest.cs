using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands
{
    public record UpdateAuthorRequest : IResultRequest
    {
        public required Guid Id { get; init; }
        public required string? Biography { get; init; }
        public required DateOnly BirthDate { get; init; }
        public required Guid? AvatarId { get; init; }
    }
}
