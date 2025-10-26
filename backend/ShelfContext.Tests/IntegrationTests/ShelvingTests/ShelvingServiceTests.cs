using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Services;

namespace ShelfContext.Tests.IntegrationTests.ShelvingTests
{
    public class ShelvingServiceTests
    {
        private ShelvingService sut = null!;

        private ShelfDbContext _db = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            sut = new ShelvingService(
                new ShelfRepository(_db),
                new BookAccessor(_db),
                new ShelvedBookRepository(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Marks_new_shelved_book_for_inserting()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelf = Shelf.Create(userId, ShelfName.Create("Shelf").Model).Model;
            var book = new Book()
            {
                Id = new BookId(Guid.NewGuid())
            };
            _db.AddRange(shelf, book);
            await _db.SaveChangesAsync();

            var result = await sut.Shelve(shelf.Id, book.Id);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Model.Value, Is.Not.EqualTo(Guid.Empty));
            Assert.That(await _db.ShelvedBooks.AnyAsync(x => x.Id == result.Model), Is.False);
        }

        [Test]
        public async Task Marks_old_shelved_book_for_update()
        {
            var userId = new UserId(Guid.NewGuid());
            var book = new Book()
            {
                Id = new BookId(Guid.NewGuid())
            };
            var shelf1 = Shelf.Create(userId, ShelfName.Create("Shelf1").Model).Model;
            var shelf2 = Shelf.Create(userId, ShelfName.Create("Shelf2").Model).Model;
            var shelvedBook = shelf1.Shelve(book.Id);
            _db.AddRange(book, shelf1, shelf2, shelvedBook);
            await _db.SaveChangesAsync();

            var result = await sut.Shelve(shelf2.Id, book.Id);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Model.Value, Is.Not.EqualTo(Guid.Empty));
            Assert.That(await _db.ShelvedBooks.AnyAsync(x => x.ShelfId == shelf2.Id), Is.False);
        }
    }
}
