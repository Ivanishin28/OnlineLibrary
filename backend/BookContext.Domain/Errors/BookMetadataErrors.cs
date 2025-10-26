using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Errors
{
    public static class BookMetadataErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("BookMetadata");

        public static Error DescriptionTooLong(int maxLength) => _errors.BuildError("Description", $"Too long {maxLength}");
    }
}
