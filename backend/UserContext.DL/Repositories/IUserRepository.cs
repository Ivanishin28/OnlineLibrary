using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Domain.Entities;

namespace UserContext.DL.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> Add(User user);
        Task Delete(User user);

        Task<User> GetBy(Guid id);
    }
}
