using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBy(BookId id);
        void Add(Book book);
        void Delete(Book book);
    }
}
