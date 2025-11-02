using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetBy(AuthorId id);
        Task<ICollection<AuthorId>> EnsureExist(ICollection<AuthorId> authorIds);
        void Add(Author author);
        void Remove(Author author);
    }
}
