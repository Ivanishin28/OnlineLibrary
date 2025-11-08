using BookContext.Contract.Dtos;
using MediatR;

namespace BookContext.Contract.Queries
{
    public record GetAllGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
    }
}

