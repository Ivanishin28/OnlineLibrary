using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateBook
{
    public class CreateBookRequest : IResultRequest<CreateBookResponse>
    {
        public string Title { get; private set; }
        public ICollection<Guid> AuthorIds { get; private set; }

        public CreateBookRequest(string title)
        {
            Title = title;
        }
    }
}
