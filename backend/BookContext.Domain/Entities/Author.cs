using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Author
    {
        public AuthorId Id { get; private set; } = null!;
        public UserId CreatorId { get; private set; } = null!;
        public FullName FullName { get; private set; } = null!;

        private Author() { }

        private Author(AuthorId id, FullName fullName, UserId creatorId)
        {
            Id = id;
            FullName = fullName;
            CreatorId = creatorId;
        }

        public static Result<Author> Create(UserId creatorId, FullName fullName)
        {
            var authorId = new AuthorId(Guid.NewGuid());

            return new Author(authorId, fullName, creatorId);
        }
    }
}
