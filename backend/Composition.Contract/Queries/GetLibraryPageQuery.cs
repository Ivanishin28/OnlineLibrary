using Composition.Contract.Dtos;
using Composition.Contract.Models;
using MediatR;
using Shared.Contracts.Models;

namespace Composition.Contract.Queries
{
    public record GetLibraryPageQuery(Guid UserId, LibraryFilter Filter, Page Page) : IRequest<Pagination<LibraryShelvedBook>>
    {
    }
}
