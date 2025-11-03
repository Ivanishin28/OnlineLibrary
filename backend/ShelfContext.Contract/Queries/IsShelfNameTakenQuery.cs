using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries
{
    public record IsShelfNameTakenQuery : IRequest<bool>
    {
        public required Guid UserId { get; init; }
        public required string Name { get; init; }
    }
}
