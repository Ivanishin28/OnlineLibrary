using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.CreateShelf
{
    public class CreateShelfResponse
    {
        public Guid CreatedShelfId { get; private set; }

        public CreateShelfResponse(Guid createdShelfId)
        {
            CreatedShelfId = createdShelfId;
        }
    }
}
