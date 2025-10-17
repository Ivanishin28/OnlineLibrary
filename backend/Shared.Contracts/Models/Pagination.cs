using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Contracts.Models
{
    public record Pagination<T>
    {
        [JsonPropertyName("total")]
        public int Total { get; private set; }
        [JsonPropertyName("items")]
        public ICollection<T> Items { get; private set; }
        
        public Pagination(int total, ICollection<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}
