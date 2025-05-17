using Shared.Core.Models;
using ShelfContext.Domain.Entities.ShelvedBooks;
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
        public ShelvedBookId ShelvedBookId { get; private set; }
        public DateTime DateAdded { get; private set; }

        private BookTag() { }

        private BookTag(BookTagId id, TagId tagId, ShelvedBookId shelvedBookId, DateTime dateAdded)
        {
            Id = id;
            TagId = tagId;
            ShelvedBookId = shelvedBookId;
            DateAdded = dateAdded;
        }

        public static BookTag Create(TagId tagId, ShelvedBookId shelvedBookId)
        {
            var id = new BookTagId(Guid.NewGuid());
            var created = DateTime.Now;

            return new BookTag(id, tagId, shelvedBookId, created);
        }
    }
}
