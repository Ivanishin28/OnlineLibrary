using MediatR;
using ShelfContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries.GetShelvesByUserId
{
    public record GetShelvesByUserIdRequest : IRequest<IEnumerable<ShelfDto>>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }

        public GetShelvesByUserIdRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}
