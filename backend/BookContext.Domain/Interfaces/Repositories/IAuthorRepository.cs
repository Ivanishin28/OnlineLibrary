using BookContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetBy(Guid id);
        Task<IEnumerable<Author>> GetByIds(IEnumerable<Guid> ids);
        void Add(Author author);
        void Remove(Author author);
    }
}
