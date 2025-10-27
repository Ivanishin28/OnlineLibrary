using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Models;

namespace ShelfContext.Contract.Queries
{
    public record GetLibraryPageQuery(Guid UserId, LibraryFilter Filter, Page Page) : IRequest<Pagination<ShelvedBookDto>>
    {
    }
}
