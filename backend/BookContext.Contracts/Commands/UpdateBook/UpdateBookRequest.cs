using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.UpdateBook
{
    public class UpdateBookRequest : IResultRequest
    {
        public Guid BookId { get; private set; }
        public string Title { get; private set; }
        public ICollection<Guid> AuthorIds { get; private set; }

        public UpdateBookRequest(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
