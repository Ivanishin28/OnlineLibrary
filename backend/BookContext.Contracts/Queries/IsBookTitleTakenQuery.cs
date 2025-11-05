using MediatR;

namespace BookContext.Contract.Queries
{
    public record IsBookTitleTakenQuery(string Title) : IRequest<bool>
    {
    }
}

