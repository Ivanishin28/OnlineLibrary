using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Commands
{
    public record SetAvatarRequest(Guid UserId, Guid AvatarId) : IRequest
    {
    }
}
