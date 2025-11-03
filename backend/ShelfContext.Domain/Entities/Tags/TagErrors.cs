using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Tags
{
    public static class TagErrors
    {
        private static ErrorBuilder _erros = new ErrorBuilder("Tag");

        public static readonly Error EmptyName =
            _erros.BuildError("Name.Empty", "Empty tag name");

        public static readonly Error TooLongName =
            _erros.BuildError("Name.TooLong", "Empty tag name too long");

        public static readonly Error NameTaken =
            _erros.BuildError("Name.Taken", "Empty tag name too long");

        public static Error NotFound(TagId tagId) =>
            _erros.BuildError("NotFound", $"Didn't find tag with Id {tagId.Value}");
    }
}
