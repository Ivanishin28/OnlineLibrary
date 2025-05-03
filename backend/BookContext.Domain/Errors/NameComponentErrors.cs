using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Errors
{
    public static class NameComponentErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("NameComponent");

        public static readonly Error LENGTH
            = _errors.BuildError("Length", "INVALID_LENGTH");

        public static readonly Error PATTERN
            = _errors.BuildError("Pattern", "IVALID_PATTERN");
    }
}
