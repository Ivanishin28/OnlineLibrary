using BookContext.Domain.Errors;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly BirthDate { get; private set; }

        private Author(string firstName, string lastName, DateOnly birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public static Result<Author> Create(string firstName, string lastName, DateOnly birthDate)
        {
            if(!IsNameValid(firstName))
            {
                return Result<Author>.Failure(AuthorErrors.FirstNameError);
            }

            if(!IsNameValid(lastName))
            {
                return Result<Author>.Failure(AuthorErrors.LastNameError);
            }

            return new Author(firstName, lastName, birthDate);
        }

        private static bool IsNameValid(string name)
        {
            return !String.IsNullOrWhiteSpace(name);
        }
    }
}
