using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.BooksOnShelves
{
    public class BookOnAShelf
    {
        public BookOnAShelfId Id { get; private set; }
        public ShelfId ShelfId { get; private set; }
        public BookId BookId { get; private set; }
        public DateTime DateShelved { get; private set; }

        private BookOnAShelf(BookOnAShelfId id, ShelfId shelfId, BookId bookId, DateTime dateShelved)
        {
            Id = id;
            ShelfId = shelfId;
            BookId = bookId;
            DateShelved = dateShelved;
        }

        public static BookOnAShelf Create(ShelfId shelfId, BookId bookId)
        {
            var id = new BookOnAShelfId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new BookOnAShelf(id, shelfId, bookId, dateCreated);
        }
    }
}
