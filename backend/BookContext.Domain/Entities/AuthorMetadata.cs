using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class AuthorMetadata
    {
        public AuthorMetadataId Id { get; private set; } = null!;
        public AuthorId AuthorId { get; private set; } = null!;
        public MediaFileId? AvatarId { get; private set; }
        public AuthorBiography? Biography { get; private set; }
        public DateOnly BirthDate { get; private set; }

        private AuthorMetadata() { }

        private AuthorMetadata(AuthorId authorId, MediaFileId? avatarId, AuthorBiography? biography, DateOnly birthDate)
        {
            Id = new AuthorMetadataId(Guid.NewGuid());
            AuthorId = authorId;
            AvatarId = avatarId;
            Biography = biography;
            BirthDate = birthDate;
        }

        public void SetAvatar(MediaFileId? avatar)
        {
            AvatarId = avatar;
        }

        public void SetBiography(AuthorBiography? biography)
        {
            Biography = biography;
        }

        public void SetBirthDate(DateOnly birthDate)
        {
            BirthDate = birthDate;
        }

        public static Result<AuthorMetadata> Create(
            AuthorId authorId, 
            MediaFileId? avatarId, 
            AuthorBiography? biography, 
            DateOnly birthDate)
        {
            return new AuthorMetadata(authorId, avatarId, biography, birthDate);
        }
    }
}
