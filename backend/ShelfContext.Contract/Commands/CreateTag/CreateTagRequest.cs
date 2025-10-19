using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.CreateTag
{
    public class CreateTagRequest : IResultRequest<Guid?>
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; private set; }
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public CreateTagRequest(Guid userId, string name)
        {
            Name = name;
            UserId = userId;
        }
    }
}
