using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Contracts.Models
{
    public record Page(
        [property: JsonPropertyName("page_index")] int PageIndex,
        [property: JsonPropertyName("page_size")] int PageSize)
    {
    }
}
