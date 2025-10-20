using BookContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBy(Guid id);
        void Add(Book book);
        void Delete(Book book);
    }
}
