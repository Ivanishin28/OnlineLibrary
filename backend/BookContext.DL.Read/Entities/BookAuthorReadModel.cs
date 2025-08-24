using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Entities
{
    public class BookAuthorReadModel
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public BookReadModel Book { get; private set; } = null!;
        public AuthorReadModel Author { get; private set; } = null!;
    }
}
