using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Domain.Entities
{
    public class User
    {
        public const int MAX_NAME_LENGTH = 32;

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly BirthDate { get; private set; }

        private User() { }

        public User(string firstName, string lastName, DateOnly birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public static Result<User> Create(string firstName, string lastName, DateOnly birthDate)
        {
            var errors = new List<string>();
            
            if(!IsValidName(firstName))
            {
                errors.Add("Invlid first name");
            }

            if(!IsValidName(lastName))
            {
                errors.Add("Invalid last name");
            }

            if(errors.Any())
            {
                return Result<User>.Failure(errors.ToArray());
            }

            return new User(firstName, lastName, birthDate);
        }

        private static bool IsValidName(string name)
        {
            return !String.IsNullOrEmpty(name) && name.Length > 0 && name.Length < MAX_NAME_LENGTH;
        }
    }
}
