using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.CreateShelf
{
    public class CreateShelfRequest : IResultRequest<CreateShelfResponse>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public CreateShelfRequest(Guid userId, string name)
        {
            Name = name;
            UserId = userId;
        }
    }
}
