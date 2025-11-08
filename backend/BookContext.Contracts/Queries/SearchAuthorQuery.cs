using BookContext.Contract.Dtos;
using MediatR;

namespace BookContext.Contract.Queries
{
    public record SearchAuthorQuery(string Query) : IRequest<ICollection<AuthorDto>>
    {
    }
}
