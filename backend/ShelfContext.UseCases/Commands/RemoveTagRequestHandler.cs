using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.RemoveTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class RemoveTagRequestHandler
        : IRequestHandler<RemoveTagRequest, Result>
    {
        public Task<Result> Handle(RemoveTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
