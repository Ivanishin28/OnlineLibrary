using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserContext.Contract.Dtos
{
    public record UserDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; init; } = null!;
        [JsonPropertyName("last_name")]
        public string LastName { get; init; } = null!;
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; init; }

        public UserDto(Guid id, string firstName, string lastName, DateOnly birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}

