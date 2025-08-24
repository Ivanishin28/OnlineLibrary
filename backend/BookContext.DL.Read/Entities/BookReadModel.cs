using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Entities
{
    public class BookReadModel
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public IReadOnlyCollection<BookAuthorReadModel> BookAuthors { get; private set; } 
            = new List<BookAuthorReadModel>();
    }
}
