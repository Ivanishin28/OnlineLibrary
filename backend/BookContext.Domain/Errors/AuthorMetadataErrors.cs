using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Errors
{
    public static class AuthorMetadataErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("AuthorMetadata");

        public static Error BiographyTooLong(int maxLenght) => _errors.BuildError("Biography", $"Toolong {maxLenght}");
    }
}
