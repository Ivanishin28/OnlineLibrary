using Shared.Core.Models;
using ShelfContext.Domain.Entities.BooksOnShelves;
using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.BookTags
{
    public class BookTag
    {
        public BookTagId Id { get; private set; }
        public TagId TagId { get; private set; }
        public BookOnAShelfId BookOnAShelfId { get; private set; }
        public DateTime DateAdded { get; private set; }

        private BookTag() { }

        private BookTag(BookTagId id, TagId tagId, BookOnAShelfId bookOnAShelfId, DateTime dateAdded)
        {
            Id = id;
            TagId = tagId;
            BookOnAShelfId = bookOnAShelfId;
            DateAdded = dateAdded;
        }

        public static BookTag Create(TagId tagId, BookOnAShelfId bookOnAShelfId)
        {
            var id = new BookTagId(Guid.NewGuid());
            var created = DateTime.Now;

            return new BookTag(id, tagId, bookOnAShelfId, created);
        }
    }
}
