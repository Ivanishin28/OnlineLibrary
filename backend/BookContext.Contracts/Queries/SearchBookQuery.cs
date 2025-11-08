using BookContext.Contract.Dtos;
using MediatR;

namespace BookContext.Contract.Queries
{
    public record SearchBookQuery(string Query) : IRequest<ICollection<BookPreviewDto>>
    {
    }
}
