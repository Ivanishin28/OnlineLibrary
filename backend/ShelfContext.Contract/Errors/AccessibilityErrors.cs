using Shared.Core.Models;

namespace ShelfContext.Contract.Errors
{
    public static class AccessibilityErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("Accessibility");

        public static Error INACCESSIBLE = _errors.BuildError("Resouce", "Error");
        public static Error CannotAccessTag(Guid userId, Guid resourceId) =>
            _errors.BuildError("Tag", $"User {userId} cannot access Tag {resourceId}");
        public static Error CannotAccessShelf(Guid userId, Guid resourceId) =>
            _errors.BuildError("Shelf", $"User {userId} cannot access Shelf {resourceId}");
        public static Error CannotAccessBook(Guid userId, Guid resourceId) =>
            _errors.BuildError("Book", $"User {userId} cannot access Book {resourceId}");
        public static Error CannotAccessShelvedBook(Guid userId, Guid resourceId) =>
            _errors.BuildError("ShelvedBook", $"User {userId} cannot access ShelvedBook {resourceId}");
    }
}
