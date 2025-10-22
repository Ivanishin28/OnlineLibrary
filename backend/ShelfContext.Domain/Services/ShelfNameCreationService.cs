using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfNameCreationService : IShelfNameCreationService
    {
        private IShelfNameUniquenessChecker _shelfNameChecker;

        public ShelfNameCreationService(IShelfNameUniquenessChecker shelfNameChecker)
        {
            _shelfNameChecker = shelfNameChecker;
        }

        public async Task<Result<ShelfName>> Create(UserId userId, string shelfName)
        {
            var nameResult = ShelfName.Create(shelfName);

            if (nameResult.IsFailure)
            {
                return nameResult.ToFailure<ShelfName>();
            }

            var isShelfNameTaken = await _shelfNameChecker.IsNameTakenBy(nameResult.Model, userId);

            if (isShelfNameTaken)
            {
                return Result<ShelfName>.Failure(ShelfErrors.DuplicateName);
            }

            return nameResult;
        }
    }
}
