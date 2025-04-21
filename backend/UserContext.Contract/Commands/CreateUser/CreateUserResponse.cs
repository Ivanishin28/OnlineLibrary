using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.CreateUser
{
    public class CreateUserResponse
    {
        public Guid UserId { get; private set; }

        public CreateUserResponse(Guid userId)
        {
            UserId = userId;
        }
    }
}
