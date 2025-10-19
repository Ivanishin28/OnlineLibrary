using Shared.Core.Models;
using ShelfContext.Application.Interfaces;
using ShelfContext.Application.Models;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Services;

namespace ShelfContext.Application.Concrete
{
    public class ResouceAuthChecker : IResouceAuthChecker
    {
        private IResouceAccessibilityChecker _checker;

        public ResouceAuthChecker(IResouceAccessibilityChecker checker)
        {
            _checker = checker;
        }

        public async Task<Result> CheckResouceAccessibilityToUser(Guid userId, Resouces resouces)
        {
            if (resouces.ShelfId is not null && 
                !(await _checker.IsShelfAccesibleToUser(resouces.ShelfId.Value, userId)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessShelf(userId, resouces.ShelfId.Value));
            }
            if (resouces.BookId is not null &&
                !(await _checker.IsBookAccessibleToUser(resouces.BookId.Value, userId)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessBook(userId, resouces.BookId.Value));
            }
            if (resouces.ShelvedBookId is not null &&
                !(await _checker.IsShelvedBookAccessibleToUser(resouces.ShelvedBookId.Value, userId)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessShelvedBook(userId, resouces.ShelvedBookId.Value));
            }
            if (resouces.TagId is not null &&
                !(await _checker.IsTagAccessibleToUser(resouces.TagId.Value, userId)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessTag(userId, resouces.TagId.Value));
            }

            return Result.Success();
        }
    }
}
