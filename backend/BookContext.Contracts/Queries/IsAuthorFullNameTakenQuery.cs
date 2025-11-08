using MediatR;

namespace BookContext.Contract.Queries
{
    public record IsAuthorFullNameTakenQuery(string FirstName, string LastName, string? MiddleName = null) : IRequest<bool>
    {
    }
}

