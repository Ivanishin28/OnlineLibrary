using Composition.Contract.Dtos;
using Composition.Contract.Models;
using MediatR;
using Shared.Contracts.Models;
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
