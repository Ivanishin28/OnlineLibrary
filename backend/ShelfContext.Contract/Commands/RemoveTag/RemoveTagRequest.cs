using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.RemoveTag
{
    public class RemoveTagRequest : IResultRequest
    {
        public Guid BookTagId { get; private set; }

        public RemoveTagRequest(Guid bookTagId)
        {
            BookTagId = bookTagId;
        }
    }
}
