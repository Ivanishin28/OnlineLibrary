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
        [JsonPropertyName("creator_id")]
        public Guid CreatorId { get; init; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; init; }
        [JsonPropertyName("last_name")]
        public string LastName { get; init; }
        [JsonPropertyName("middle_name")]
        public string? MiddleName { get; init; }
        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; init; }
        [JsonPropertyName("avatar_id")]
        public Guid? AvatarId { get; init; }
        [JsonPropertyName("biography")]
        public string? Biography { get; init; }

        public AuthorDto(Guid id, Guid creatorId, string firstName, string lastName, string? middleName, DateOnly birthDate, Guid? avatarId, string? biography)
        {
            Id = id;
            CreatorId = creatorId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDate = birthDate;
            AvatarId = avatarId;
            Biography = biography;
        }
    }
}
