using BookContext.Domain.Constants;
using BookContext.Domain.Errors;
using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public class NameComponent : ValueObject
    {
        public string Value { get; private set; }

        private NameComponent(string value)
        {
            Value = value;
        }

        public static Result<NameComponent> Create(string? value)
        {
            if (String.IsNullOrEmpty(value) || value.Length > AuthorConstants.MAX_NAME_COMPONENT_LENGTH)
            {
                return Result<NameComponent>.Failure(NameComponentErrors.LENGTH);
            }

            if(!IsValid(value))
            {
                return Result<NameComponent>.Failure(NameComponentErrors.PATTERN);
            }

            return new NameComponent(value);
        }

        private static bool IsValid(string value)
        {
            var pattern = "^[A-Z][a-z]*('?[A-Z][a-z]+)*$";
            return Regex.IsMatch(value, pattern);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return [Value];
        }
    }
}
