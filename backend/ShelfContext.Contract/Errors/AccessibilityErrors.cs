using Shared.Core.Models;

namespace ShelfContext.Contract.Errors
{
    public static class AccessibilityErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("Accessibility");

        public static Error CannotAccessTag(Guid userId, Guid resourceId) =>
            _errors.BuildError("Tag", $"User {userId} cannot access Tag {resourceId}");
        public static Error CannotAccessShelf(Guid userId, Guid resourceId) =>
            _errors.BuildError("Shelf", $"User {userId} cannot access Shelf {resourceId}");
        public static Error CannotAccessBook(Guid userId, Guid resourceId) =>
            _errors.BuildError("Book", $"User {userId} cannot access Book {resourceId}");
    }
}
