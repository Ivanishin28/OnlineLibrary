using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.BooksOnShelves
{
    public static class BookOnAShelfErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("BookOnAShelf");

        public static readonly Error AlreadyTagged =
            _errors.BuildError("Tag.AlreadyTagger", "Tag.AlreadyTagger");
    }
}
