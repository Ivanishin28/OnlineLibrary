using MediatR;
using Shared.Contracts.Interfaces;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.AddTagToBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class AddTagToBookRequestHandler
        : IRequestHandler<AddTagToBookRequest, Result<AddTagToBookResponse>>
    {
        public Task<Result<AddTagToBookResponse>> Handle(AddTagToBookRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
