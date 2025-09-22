using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IShelvedBookRepository
    {
        Task<ShelvedBook?> GetBy(ShelvedBookId id);
        Task<ShelvedBook?> GetBy(UserId userId, BookId bookId);

        void Add(ShelvedBook shelvedBook);
        void Remove(ShelvedBook shelvedBook);
    }
}
