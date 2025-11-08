using MediatR;
using Shared.Core.Models;

namespace BookContext.Contract.Commands
{
    public record ReportBookFileRequest(Guid BookId) : IRequest<Result>
    {
    }
}
