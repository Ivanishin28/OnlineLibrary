using Shared.Contracts.Interfaces;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public record CreateAuthorRequest(
        Guid CreatorId, 
        string FirstName, 
        string LastName, 
        DateOnly BirthDate,
        Guid? AvatarId,
        string? Biography) : IResultRequest<Guid?>
    {
    }
}
