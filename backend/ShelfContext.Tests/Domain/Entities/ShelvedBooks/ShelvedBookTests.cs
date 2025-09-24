using Shared.Core.Extensions;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.Domain.Entities.ShelvedBooks
{
    public class ShelvedBookTests
    {
        private ShelvedBook _shelvedBook;

        [SetUp]
        public void SetUp()
        {
            var bookId = new BookId(Guid.NewGuid());
            var shelfId = new ShelfId(Guid.NewGuid());
            var userId = new UserId(Guid.NewGuid());
            _shelvedBook = ShelvedBook.Create(shelfId, bookId, userId);
        }

        [Test]
        public void AddTag_FirstTag_ReturnsSuccess()
        {
            var tag = CreateTagFrom("first");

            var addResult = _shelvedBook.Add(tag);

            Assert.That(addResult.IsSuccess);
            Assert.That(_shelvedBook.BookTags.Count == 1);
        }

        [Test]
        public void AddTag_SameTagTwice_ReturnsFailure()
        {
            var tag = CreateTagFrom("first");

            var addResult = _shelvedBook.Add(tag);
            Assert.That(addResult.IsSuccess);

            var secondAddResult = _shelvedBook.Add(tag);
            Assert.That(secondAddResult.HasError(ShelvedBookErrors.AlreadyTagged));
            Assert.That(_shelvedBook.BookTags.Count == 1);
        }

        [Test]
        public void RemoveTag_FirstTag_ReturnsSuccess()
        {
            var tag1 = CreateTagFrom("first");
            var tag2 = CreateTagFrom("second");

            _shelvedBook.Add(tag1);
            _shelvedBook.Add(tag2);

            Assert.That(_shelvedBook.BookTags.Count == 2);

            var removeResult = _shelvedBook.Remove(tag1.Id);
            Assert.That(removeResult.IsSuccess);
            Assert.That(_shelvedBook.BookTags.Count == 1);
        }

        [Test]
        public void RemoveTag_SameTag_ReturnsFailure()
        {
            var tag1 = CreateTagFrom("first");
            var tag2 = CreateTagFrom("second");

            _shelvedBook.Add(tag1);
            _shelvedBook.Add(tag2);

            Assert.That(_shelvedBook.BookTags.Count == 2);

            var removeResult = _shelvedBook.Remove(tag1.Id);
            Assert.That(removeResult.IsSuccess);
            Assert.That(_shelvedBook.BookTags.Count == 1);

            var secondRemoveResult = _shelvedBook.Remove(tag1.Id);
            Assert.That(secondRemoveResult.IsFailure);
            Assert.That(_shelvedBook.BookTags.Count == 1);
        }

        private Tag CreateTagFrom(string name)
        {
            var tagNameResult = TagName.Create(name);
            Assert.That(tagNameResult.IsSuccess);

            var tagResult = Tag.Create(tagNameResult.Model);
            Assert.That(tagResult.IsSuccess);

            return tagResult.Model;
        }
    }
}
