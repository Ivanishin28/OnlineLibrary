using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.RegisterUserAccount
{
    public class RegisterUserAccountRequest : IRequest<RegisterUserAccountResponse>
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public RegisterUserAccountRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
