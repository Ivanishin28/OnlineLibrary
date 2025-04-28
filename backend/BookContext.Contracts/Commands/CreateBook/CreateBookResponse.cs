using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateBook
{
    public class CreateBookResponse
    {
        public Guid BookId { get; private set; }

        public CreateBookResponse(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
