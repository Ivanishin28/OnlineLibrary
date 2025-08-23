using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Dtos
{
    public record AuthorDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; init; }
        [JsonPropertyName("last_name")]
        public string LastName { get; init; }
        [JsonPropertyName("middle_name")]
        public string? MiddleName { get; init; }
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; init; }

        public AuthorDto(Guid id, string firstName, string lastName, string? middleName, DateOnly birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }
    }
}
