using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Queries
{
    public interface IBookShelvedChecker
    {
        Task<bool> IsBookShelvedBy(BookId bookId, UserId userId);
    }
}
