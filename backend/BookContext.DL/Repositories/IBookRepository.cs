using BookContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBy(Guid id);
        Task Add(Book book);
        Task Delete(Book book);
    }
}
