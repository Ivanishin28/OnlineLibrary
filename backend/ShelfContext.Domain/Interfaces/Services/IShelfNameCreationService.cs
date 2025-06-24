using Shared.Core.Models;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Services
{
    public interface IShelfNameCreationService
    {
        Task<Result<ShelfName>> Create(UserId userId, string shelfName);
    }
}
