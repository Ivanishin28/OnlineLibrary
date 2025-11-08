using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands
{
    public record DeleteAuthorRequest(Guid AuthorId) : IResultRequest
    {
    }
}
