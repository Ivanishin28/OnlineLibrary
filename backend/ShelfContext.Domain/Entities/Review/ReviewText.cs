using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Review
{
    public class ReviewText : ValueObject
    {
        public const int MAX_LENGTH = 5000;

        public string? Value { get; private set; }

        private ReviewText(string? value)
        {
            Value = value;
        }

        public static Result<ReviewText> Create(string? text)
        {
            if (text is not null && text.Length > 5000)
            {
                return Result<ReviewText>.Failure(ReviewErrors.ReviewTextTooLong(MAX_LENGTH));
            }

            return new ReviewText(text);
        }

        public static ReviewText Blank => new ReviewText(null);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            return new[] { Value };
        }
    }
}
