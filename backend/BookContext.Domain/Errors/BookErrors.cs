using Shared.Core.Models;

namespace BookContext.Domain.Errors
{
    public class BookErrors
    {
        private static ErrorBuilder _error = new ErrorBuilder("Book");

        public static readonly Error EmptyAuthorList 
            = _error.BuildError("Authors.Empty", "EmptyAuthorList");

        public static readonly Error DuplicateAuthors 
            = _error.BuildError("Authors.Duplicates", "DuplicateAuthors");

        public static readonly Error DifferentBookAuthor
            = _error.BuildError("Authors.DifferentBook", "DifferentBookAuthor");
    }
}
