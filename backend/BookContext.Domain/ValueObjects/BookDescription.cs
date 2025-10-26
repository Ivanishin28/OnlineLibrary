using BookContext.Domain.Errors;
using Shared.Core.Models;
using Shared.Core.ValueObjects;

namespace BookContext.Domain.ValueObjects
{
    public class BookDescription : ValueObject
    {
        public const int MAX_LENGTH = 5000;

        public string? Value { get; private set; } = null;

        private BookDescription() { }

        private BookDescription(string? value)
        {
            Value = value;
        }

        public static Result<BookDescription> Create(string? value)
        {
            if (value is not null && value.Length > MAX_LENGTH)
            {
                return Result<BookDescription>.Failure(BookMetadataErrors.DescriptionTooLong(MAX_LENGTH));
            }

            return new BookDescription(value);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            return new[] { Value };
        }
    }
}
