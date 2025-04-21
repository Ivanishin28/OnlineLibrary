using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.RegisterUserAccount
{
    public class RegisterUserAccountResponse
    {
        public Guid UserAccountId { get; private set; }

        public RegisterUserAccountResponse(Guid userAccountId)
        {
            UserAccountId = userAccountId;
        }
    }
}
