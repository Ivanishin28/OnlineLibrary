using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands
{
    public record DeleteTagRequest : IResultRequest
    {
        public Guid TagId { get; init; }

        public DeleteTagRequest(Guid tagId)
        {
            TagId = tagId;
        }
    }
}
