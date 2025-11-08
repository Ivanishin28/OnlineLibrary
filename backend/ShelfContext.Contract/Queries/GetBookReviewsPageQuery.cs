using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries
{
    public record GetBookReviewsPageQuery(
        [property: JsonPropertyName("book_id")] Guid BookId, 
        [property: JsonPropertyName("page")] Page Page) : IRequest<Pagination<ReviewDto>>
    {
    }
}
