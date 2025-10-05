using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using System.Collections.Immutable;

namespace ShelfContext.Domain.Entities.ShelvedBooks
{
    public class ShelvedBook
    {
        private List<BookTag> _bookTags = new();

        public ShelvedBookId Id { get; private set; } = null!;
        public UserId UserId { get; private set; } = null!;
        public ShelfId ShelfId { get; private set; } = null!;
        public BookId BookId { get; private set; } = null!;
        public DateTime DateShelved { get; private set; }

        public IReadOnlyCollection<BookTag> BookTags => _bookTags.AsReadOnly();

        public ShelvedBook() { }

        private ShelvedBook(ShelvedBookId id, ShelfId shelfId, BookId bookId, DateTime dateShelved, UserId userId)
        {
            Id = id;
            ShelfId = shelfId;
            BookId = bookId;
            DateShelved = dateShelved;
            UserId = userId;
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

            if(tagToRemove is null)
            {
                return Result.Failure(ShelvedBookErrors.TagNotFound);
            }
            
            _bookTags.Remove(tagToRemove);

            return Result.Success();
        }

        public Result ReshelveTo(Shelf shelf)
        {
            if (shelf.UserId != UserId)
            {
                return Result.Failure(ShelvedBookErrors.RESHELVE_TO_OTHER_USER);
            }

            ShelfId = shelf.Id;
            DateShelved = TimeExtensions.Now();

            return Result.Success();
        }

        public static ShelvedBook Create(ShelfId shelfId, BookId bookId, UserId userId)
        {
            var id = new ShelvedBookId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new ShelvedBook(id, shelfId, bookId, dateCreated, userId);
        }
    }
}
