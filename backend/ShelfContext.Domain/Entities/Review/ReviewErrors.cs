using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Review
{
    public static class ReviewErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("Review");

        public static Error ReviewTextTooLong(int maxLength)
            => _errors.BuildError("Text.Length", $"Max length = ${maxLength}");

        public static readonly Error INVALID_RATING = _errors.BuildError("Rating.Invalid", "Invalid Rating");

        public static readonly Error REVIEW_ONLY_SHELVED_BOOKS = _errors.BuildError("Book.NotShelved", "Book is not shelved");

        public static readonly Error USER_ALREADY_REVIEWED = _errors.BuildError("Review.Exists", "Book is already reviewed");
    }
}
