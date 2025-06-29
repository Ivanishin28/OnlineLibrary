using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class BookTagReadModel : ReadModel
    {
        public Guid TagId { get; set; }
        public Guid ShelvedBookId { get; set; }
        public DateTime DateAdded { get; set; }

        public TagReadModel Tag { get; set; }
        public ShelvedBookReadModel ShelvedBook { get; set; }
    }
}
