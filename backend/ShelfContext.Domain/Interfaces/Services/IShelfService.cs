using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;

namespace ShelfContext.Domain.Interfaces.Services
{
    public interface IShelfService
    {
        Task<Result<ShelvedBook>> ShelveBook(ShelfId shelfId, BookId bookId);
    }
}
