using Shared.Contracts.Interfaces;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public record CreateAuthorRequest : IResultRequest<Guid?>
    {
        public required Guid CreatorId { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public DateOnly BirthDate { get; init; }
        public Guid? AvatarId { get; init; }
        public string? Biography { get; init; }
    }
}
