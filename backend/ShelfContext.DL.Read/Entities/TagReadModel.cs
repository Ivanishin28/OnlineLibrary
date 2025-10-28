using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class TagReadModel : ReadModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }

        public IEnumerable<BookTagReadModel> BookTags { get; set; } = null!;
    }
}
