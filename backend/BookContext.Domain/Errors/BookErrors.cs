using Shared.Core.Models;

namespace BookContext.Domain.Errors
{
    public class BookErrors
    {
        private static ErrorBuilder _error = new ErrorBuilder("Book");

        public static readonly Error DuplicateAuthors 
            = _error.BuildError("Authors.Duplicates", "DuplicateAuthors");

        public static readonly Error DifferentBookAuthor
            = _error.BuildError("Authors.DifferentBook", "DifferentBookAuthor");

        public static readonly Error EmptyTitle
            = _error.BuildError("Title.Empty", "Title.Empty");
    }
}
