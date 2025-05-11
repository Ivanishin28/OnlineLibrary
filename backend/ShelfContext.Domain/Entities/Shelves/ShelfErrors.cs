using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Shelves
{
    public static class ShelfErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("Shelf");

        public static readonly Error EmptyShelfName =
            _errors.BuildError("Name.Empty", "Name.Empty");

        public static readonly Error LongShelfName =
            _errors.BuildError("Name.TooLong", "Too long");

        public static readonly Error AlreadyShelved =
            _errors.BuildError("Book.Shelved", "Book.Shelved");

        public static readonly Error AlreadyShelvedOnThisShelf =
            _errors.BuildError("Book.Shelved.Here", "Book.AlreadyShelved");

        public static readonly Error AlreadyShelvedOnAnotherShelf =
            _errors.BuildError("Book.Shelved.Elsewhere", "Book.AlreadyShelved");
    }
}
