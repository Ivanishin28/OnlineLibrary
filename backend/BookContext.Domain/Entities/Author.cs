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
        public DateOnly BirthDate { get; private set; }

        private Author() { }

        private Author(AuthorId id, FullName fullName, DateOnly birthDate, UserId creatorId)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            CreatorId = creatorId;
        }

        public static Result<Author> Create(UserId creatorId, FullName fullName, DateOnly birthDate)
        {
            var authorId = new AuthorId(Guid.NewGuid());

            if(fullName is null)
            {
                return Result<Author>.Failure(AuthorErrors.FullNameError);
            }

            return new Author(authorId, fullName, birthDate, creatorId);
        }
    }
}
