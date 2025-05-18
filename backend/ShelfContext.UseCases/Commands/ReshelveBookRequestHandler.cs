using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ReshelveBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class ReshelveBookRequestHandler
        : IRequestHandler<ReshelveBookRequest, Result>
    {
        public Task<Result> Handle(ReshelveBookRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
