using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Domain.Entities
{
    public class UserAccount
    {
        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public Email Email { get; private set; }
        public Guid ProfileId { get; private set; }

        private UserAccount() { }

        private UserAccount(string login, string password, Email email, Guid profileId)
        {
            Login = login;
            Password = password;
            Email = email;
            ProfileId = profileId;
        }

        public static Result<UserAccount> Create(string login, string password, string email, Guid profileId)
        {
            var result = Email.CreateFrom(email);

            if(!result.IsSuccess)
            {
                return Result<UserAccount>.Failure(result.Errors.ToArray());
            }

            return new UserAccount(login, password, result.Model, profileId);
        }
    }
}