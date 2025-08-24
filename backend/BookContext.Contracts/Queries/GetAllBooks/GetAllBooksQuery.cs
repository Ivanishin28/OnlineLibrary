using BookContext.Contract.Dtos;
using MediatR;

namespace BookContext.Contract.Queries.GetAllBooks
{
    public record GetAllBooksQuery : IRequest<IEnumerable<BookPreviewDto>>
    {
    }
}
