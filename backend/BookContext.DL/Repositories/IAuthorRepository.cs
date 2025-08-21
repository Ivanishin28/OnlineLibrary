using BookContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetBy(Guid id);
        Task<IEnumerable<Author>> GetByIds(IEnumerable<Guid> ids);
        Task Add(Author author);
        Task Delete(Author author);
    }
}
