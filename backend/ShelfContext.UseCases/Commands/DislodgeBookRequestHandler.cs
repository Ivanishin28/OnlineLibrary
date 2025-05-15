using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.DislodgeBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class DislodgeBookRequestHandler
        : IRequestHandler<DislodgeBookRequest, Result>
    {
        public Task<Result> Handle(DislodgeBookRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
