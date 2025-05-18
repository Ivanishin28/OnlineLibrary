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

namespace ShelfContext.Domain.Entities.ShelvedBooks
{
    public class ShelvedBook
    {
        private List<BookTag> _bookTags = new();
        
        public ShelvedBookId Id { get; private set; }
        public ShelfId ShelfId { get; private set; }
        public BookId BookId { get; private set; }
        public DateTime DateShelved { get; private set; }

        public IImmutableList<BookTag> BookTags => _bookTags.ToImmutableList();

        private ShelvedBook(ShelvedBookId id, ShelfId shelfId, BookId bookId, DateTime dateShelved)
        {
            Id = id;
            ShelfId = shelfId;
            BookId = bookId;
            DateShelved = dateShelved;
        }

        public Result Add(Tag tag)
        {
            var alreadyTagged = _bookTags
                .Any(bookTag => 
                    bookTag.TagId == tag.Id);

            if(alreadyTagged)
            {
                return Result.Failure(ShelvedBookErrors.AlreadyTagged);
            }

            var bookTag = BookTag.Create(tag.Id, Id);
            _bookTags.Add(bookTag);

            return Result.Success();
        }

        public Result Remove(TagId tagId)
        {
            var tagToRemove = _bookTags
                .FirstOrDefault(tag => tag.TagId == tagId);

            if(tagToRemove is not null)
            {
                _bookTags.Remove(tagToRemove);
            }

            return Result.Success();
        }

        public static ShelvedBook Create(ShelfId shelfId, BookId bookId)
        {
            var id = new ShelvedBookId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new ShelvedBook(id, shelfId, bookId, dateCreated);
        }
    }
}
