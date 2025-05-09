using Shared.Core.Extensions;
using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public NameComponent FirstName { get; private set; }
        public NameComponent LastName { get; private set; }
        public NameComponent? MiddleName { get; private set; }

        private FullName(NameComponent firstName, NameComponent lastName, NameComponent? middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public static Result<FullName> Create(string firstName, string lastName, string? middleName = null)
        {
            var firstNameResult = NameComponent.Create(firstName);
            var lastNameResult = NameComponent.Create(lastName);
            Result<NameComponent> middleNameResult = middleName is not null 
                ? NameComponent.Create(middleName) 
                : Result<NameComponent>.Success(null);

            var combinedResult = ResultExtensions
                .Combine(firstNameResult, lastNameResult, middleNameResult);

            if(combinedResult.IsFailure)
            {
                return combinedResult.ToFailure<FullName>();
            }

            return new FullName(
                firstNameResult.Model,
                lastNameResult.Model,
                middleNameResult.Model);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return [FirstName, LastName, MiddleName];
        }
    }
}
