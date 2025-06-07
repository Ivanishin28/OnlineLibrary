using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.ShelvedBooks
{
    public static class ShelvedBookErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("ShelvedBook");

        public static readonly Error AlreadyTagged =
            _errors.BuildError("Tag.AlreadyTagger", "Tag.AlreadyTagger");

        public static readonly Error TagNotFound =
            _errors.BuildError("Tag.NotFound", "Tag NotFound");
    }
}
