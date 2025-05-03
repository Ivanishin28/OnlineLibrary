using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public class CreateAuthorRequest : IResultRequest<CreateAuthorResponse>
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; private set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; private set; }
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; private set; }

        public CreateAuthorRequest(string firstName, string lastName, DateOnly birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
