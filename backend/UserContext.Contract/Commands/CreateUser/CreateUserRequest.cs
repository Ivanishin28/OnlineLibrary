using MediatR;
using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.CreateUser
{
    public class CreateUserRequest : IResultRequest<CreateUserResponse>
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; private set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; private set; }
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; private set; }
        
        public CreateUserRequest(string firstName, string lastName, DateOnly birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
