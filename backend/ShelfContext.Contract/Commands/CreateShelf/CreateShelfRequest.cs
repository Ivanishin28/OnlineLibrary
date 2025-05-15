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
        [JsonPropertyName("client_id")]
        public Guid ClientId { get; private set; }
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public CreateShelfRequest(Guid clientId, string name)
        {
            Name = name;
            ClientId = clientId;
        }
    }
}
