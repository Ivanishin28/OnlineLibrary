using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.CreateShelf
{
    public class CreateShelfResult
    {
        public Guid CreatedShelfId { get; private set; }

        public CreateShelfResult(Guid createdShelfId)
        {
            CreatedShelfId = createdShelfId;
        }
    }
}
