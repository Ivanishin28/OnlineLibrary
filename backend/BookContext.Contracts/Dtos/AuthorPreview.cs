using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Dtos
{
    public class AuthorPreview
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; init; } = null!;
        [JsonPropertyName("last_name")]
        public string LastName { get; init; } = null!;
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; init; }
    }
}
