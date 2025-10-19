using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries
{
    public record IsTagNameTakenQuery(Guid UserId, string TagName) : IRequest<bool>
    {
    }
}
