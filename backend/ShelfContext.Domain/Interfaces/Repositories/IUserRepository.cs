using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetBy(UserId userId);
        Task<bool> Exists(UserId userId);
    }
}
