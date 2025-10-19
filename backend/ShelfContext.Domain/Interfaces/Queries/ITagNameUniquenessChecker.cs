using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Queries
{
    public interface ITagNameUniquenessChecker
    {
        Task<bool> IsNameTakenBy(TagName name, UserId userId);
    }
}
