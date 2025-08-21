using ShelfContext.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IBookRepositroy
    {
        Task<Book?> GetBy(BookId bookId);
    }
}
