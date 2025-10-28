using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class ReviewReadModel : ReadModel
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Rating { get; set; }
        public string? Text { get; set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
