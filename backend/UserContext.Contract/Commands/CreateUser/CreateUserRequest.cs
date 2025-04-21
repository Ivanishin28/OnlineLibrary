using MediatR;
using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Contract.Commands.CreateUser
{
    public class CreateUserRequest : IResultRequest<CreateUserResponse>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly BirthDate { get; private set; }
    }
}
