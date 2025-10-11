using MediatR;
using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands
{
    public record DeleteShelfRequest : IResultRequest
    {
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; init; }

        public DeleteShelfRequest(Guid shelfId)
        {
            ShelfId = shelfId;
        }
    }
}
