using System;

namespace BookContext.DL.Read.Entities
{
    public class AuthorMetadataReadModel
    {
        public Guid Id { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid? AvatarId { get; private set; }
        public string? Biography { get; private set; }

        public AuthorReadModel Author { get; private set; } = null!;
    }
}


