using Shared.Contracts.Interfaces;

namespace ShelfContext.Contract.Commands
{
    public record RenameTagRequest : IResultRequest
    {
        public required Guid TagId { get; init; }
        public required string Name { get; init; }
    }
}

