using BookContext.Contract.Dtos;
using MediatR;

namespace BookContext.Contract.Queries
{
    public record GetBookPreviewListQuery(ICollection<Guid> BookIds) : IRequest<ICollection<BookPreviewDto>>
    {
    }
}
