using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetBy(AuthorId id);
        Task<bool> IsFullNameTaken(FullName fullname);
        Task<ICollection<AuthorId>> EnsureExist(ICollection<AuthorId> authorIds);
        void Add(Author author);
        void Remove(Author author);
    }
}
