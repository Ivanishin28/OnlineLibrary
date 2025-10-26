using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Review
{
    public sealed class Rating : ValueObject
    {
        public int Value { get; }

        private Rating(int value)
        {
            Value = value;
        }

        public static Result<Rating> Create(int value)
        {
            if (value < ReviewConsts.MIN_RATING || value > ReviewConsts.MAX_RATING)
            {
                return Result<Rating>.Failure(ReviewErrors.INVALID_RATING);
            }

            return new Rating(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
