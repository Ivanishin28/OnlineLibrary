using Composition.Contract.Dtos;
using MediatR;
using Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Composition.Contract.Queries
{
    public record GetBookReviewsWithIdentitiesQuery(
        [property: JsonPropertyName("book_id")] Guid BookId,
        [property: JsonPropertyName("page")] Page Page) : IRequest<Pagination<BookReviewWithIdentity>>
    {
    }
}
