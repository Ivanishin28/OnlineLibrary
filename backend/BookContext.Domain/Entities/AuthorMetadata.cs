using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Entities
{
    public class AuthorMetadata
    {
        public AuthorMetadataId Id { get; private set; } = null!;
        public AuthorId AuthorId { get; private set; } = null!;
        public MediaFileId? AvatarId { get; private set; }
        public AuthorBiography? Biography { get; private set; }

        private AuthorMetadata() { }

        public AuthorMetadata(AuthorMetadataId id, AuthorId authorId, MediaFileId? avatarId, AuthorBiography? biography)
        {
            Id = id;
            AuthorId = authorId;
            AvatarId = avatarId;
            Biography = biography;
        }
    }
}
