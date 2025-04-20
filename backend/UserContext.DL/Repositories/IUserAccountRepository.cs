using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Domain.Entities;

namespace UserContext.DL.Repositories
{
    public interface IUserAccountRepository
    {
        public Task<Guid> Add(UserAccount account);
        public Task Delete(UserAccount account);

        public Task<UserAccount> GetBy(Guid id);
        public Task<bool> IsUnique(Email email);
        public Task<bool> IsUnique(string login);
    }
}
