using BookContext.Contract.Dtos;
using MediatR;
using Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Queries
{
    public record GetBookPageQuery : IRequest<Pagination<BookPreviewDto>>
    {
        [JsonPropertyName("filter")]
        public required BookFilter Filter { get; init; }
        [JsonPropertyName("page")]
        public required Page Page { get; init; }
    }
}
