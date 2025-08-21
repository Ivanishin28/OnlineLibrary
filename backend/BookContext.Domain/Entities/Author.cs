using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; private set; }
        public FullName FullName { get; private set; } = null!;
        public DateOnly BirthDate { get; private set; }

        private Author() { }

        private Author(FullName fullName, DateOnly birthDate)
        {
            FullName = fullName;
            BirthDate = birthDate;
        }

        public static Result<Author> Create(FullName fullName, DateOnly birthDate)
        {
            if(fullName is null)
            {
                return Result<Author>.Failure(AuthorErrors.FullNameError);
            }

            return new Author(fullName, birthDate);
        }
    }
}
