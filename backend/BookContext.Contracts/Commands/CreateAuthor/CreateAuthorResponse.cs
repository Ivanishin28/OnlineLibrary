using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public class CreateAuthorResponse
    {
        public Guid AuthorId { get; private set; }

        public CreateAuthorResponse(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}
