using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class ShelvedBookReadModel : ReadModel
    {
        public Guid ShelfId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DateShelved { get; set; }

        public ShelfReadModel Shelf { get; set; } = null!;
        public BookReadModel Book { get; set; } = null!;
        public IEnumerable<BookTagReadModel> BookTags { get; set; } = null!;
    }
}
