using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands
{
    public record RenameShelfRequest : IRequest<Result>
    {
        public required Guid ShelfId { get; init; }
        public required string Name { get; init; }
    }
}
