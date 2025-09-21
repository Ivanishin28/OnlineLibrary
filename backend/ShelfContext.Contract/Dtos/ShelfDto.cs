using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Dtos
{
    public record ShelfDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; private set; }
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public ShelfDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
