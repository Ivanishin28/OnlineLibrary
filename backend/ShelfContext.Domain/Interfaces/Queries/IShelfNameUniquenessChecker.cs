using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Queries
{
    public interface IShelfNameUniquenessChecker
    {
        Task<bool> IsNameTakenBy(ShelfName name, UserId userId);
    }
}
