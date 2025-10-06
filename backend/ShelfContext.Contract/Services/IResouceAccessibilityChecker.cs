namespace ShelfContext.Contract.Services
{
    public interface IResouceAccessibilityChecker
    {
        Task<bool> IsTagAccessibleToUser(Guid tagId, Guid userId);
        Task<bool> IsShelfAccesibleToUser(Guid shelfId, Guid userId);
        Task<bool> IsBookAccessibleToUser(Guid bookId, Guid userId);
        Task<bool> IsShelvedBookAccessibleToUser(Guid shelvedBookId, Guid userId);
    }
}
