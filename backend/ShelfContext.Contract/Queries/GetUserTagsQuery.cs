using MediatR;
using ShelfContext.Contract.Dtos;

namespace ShelfContext.Contract.Queries
{
    public record GetUserTagsQuery(Guid UserId) : IRequest<ICollection<TagDto>>
    {
    }
}
