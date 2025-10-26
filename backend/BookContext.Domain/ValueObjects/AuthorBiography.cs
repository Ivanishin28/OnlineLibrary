using BookContext.Domain.Errors;
using Shared.Core.Extensions;
using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public class AuthorBiography : ValueObject
    {
        public const int MAX_LENGTH = 5000;

        public string Value { get; private set; } = null!;

        private AuthorBiography() { }

        private AuthorBiography(string value)
        {
            Value = value;
        }

        public static Result<AuthorBiography> Create(string value)
        {
            if (String.IsNullOrEmpty(value) || value.Length > MAX_LENGTH)
            {
                return Result<AuthorBiography>.Failure(AuthorMetadataErrors.BiographyTooLong(MAX_LENGTH));
            }

            return new AuthorBiography(value);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            return new[] { Value };
        }
    }
}
