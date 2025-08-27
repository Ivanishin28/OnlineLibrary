using IdentityContext.Contracts.Commands;
using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.UseCases.Commands
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
