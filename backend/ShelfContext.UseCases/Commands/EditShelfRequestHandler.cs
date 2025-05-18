using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.EditShelf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class EditShelfRequestHandler
        : IRequestHandler<EditShelfRequest, Result>
    {
        public Task<Result> Handle(EditShelfRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
