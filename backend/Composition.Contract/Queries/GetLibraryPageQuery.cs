using Composition.Contract.Dtos;
using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Models;
using System.Text.Json.Serialization;

namespace Composition.Contract.Queries
{
    public record GetLibraryPageQuery(
        [property: JsonPropertyName("user_id")] Guid UserId,
        [property: JsonPropertyName("filter")] LibraryFilter Filter,
        [property: JsonPropertyName("page")] Page Page) : IRequest<Pagination<LibraryShelvedBook>>
    {
    }
}
