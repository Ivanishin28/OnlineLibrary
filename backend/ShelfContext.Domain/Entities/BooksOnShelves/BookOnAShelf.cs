using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.BooksOnShelves
{
    public class BookOnAShelf
    {
        private List<BookTag> _bookTags = new();
        
        public BookOnAShelfId Id { get; private set; }
        public ShelfId ShelfId { get; private set; }
        public BookId BookId { get; private set; }
        public DateTime DateShelved { get; private set; }

        public IImmutableList<BookTag> BookTags => _bookTags.ToImmutableList();

        private BookOnAShelf(BookOnAShelfId id, ShelfId shelfId, BookId bookId, DateTime dateShelved)
        {
            Id = id;
            ShelfId = shelfId;
            BookId = bookId;
            DateShelved = dateShelved;
        }

        public Result AddTag(Tag tag)
        {
            var alreadyTagged = _bookTags
                .Any(bookTag => 
                    bookTag.TagId == tag.Id);

            if(alreadyTagged)
            {
                return Result.Failure(BookOnAShelfErrors.AlreadyTagged);
            }

            var bookTag = BookTag.Create(tag.Id, Id);
            _bookTags.Add(bookTag);

            return Result.Success();
        }

        public static BookOnAShelf Create(ShelfId shelfId, BookId bookId)
        {
            var id = new BookOnAShelfId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new BookOnAShelf(id, shelfId, bookId, dateCreated);
        }
    }
}
