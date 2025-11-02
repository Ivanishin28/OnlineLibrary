using MediatR;
using UserContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Contract.Queries
{
    public record GetUserByUserIdQuery(Guid UserId) : IRequest<UserDto?>
    {
    }
}

