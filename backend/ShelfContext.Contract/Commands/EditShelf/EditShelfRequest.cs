using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.EditShelf
{
    public class EditShelfRequest : IResultRequest
    {
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; private set; }
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public EditShelfRequest(Guid shelfId)
        {
            ShelfId = shelfId;
        }
    }
}
