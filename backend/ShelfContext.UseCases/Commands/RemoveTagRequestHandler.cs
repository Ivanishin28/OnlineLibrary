using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.RemoveTag;

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
