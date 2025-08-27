using MediatR;
using Shared.Contracts.Interfaces;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Commands
{
    public record RegisterRequest : IResultRequest<Guid>
    {
    }
}
