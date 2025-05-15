using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ShelveBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class ShelveBookRequestHandler
        : IRequestHandler<ShelveBookRequest, Result>
    {
        public Task<Result> Handle(ShelveBookRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
