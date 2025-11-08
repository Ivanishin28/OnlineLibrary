using IdentityContext.Contracts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Queries
{
    public record GetIdentityPreviewByUserIdQuery(Guid UserId) : IRequest<IdentityPreviewDto?>
    {
    }
}

