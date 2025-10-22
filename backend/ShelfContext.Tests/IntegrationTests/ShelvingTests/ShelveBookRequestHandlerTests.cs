using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Services;
using ShelfContext.Tests.Fakes;
using ShelfContext.UseCases.Commands;

namespace ShelfContext.Tests.IntegrationTests.ShelvingTests
{
    internal class ShelveBookRequestHandlerTests
    {
        private ShelveBookRequestHandler sut = null!;

        private ShelfDbContext _db = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            var service = new ShelvingService(
                new ShelfRepository(_db), 
                new BookAccessor(_db), 
                new ShelvedBookRepository(_db));

            sut = new ShelveBookRequestHandler(
                new UnitOfWork(_db),
                new AllAccessibleResouceAccessibilityChecker(),
                service);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Creates_new_ShelvedBook()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelf = Shelf.Create(userId, ShelfName.Create("Shelf").Model).Model;
            var book = new Book()
            {
                Id = new BookId(Guid.NewGuid())
            };
            _db.AddRange(shelf, book);
            await _db.SaveChangesAsync();

            var command = new ShelveBookRequest(book.Id.Value, shelf.Id.Value, userId.Value);
            var result = await sut.Handle(command, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Model, Is.Not.EqualTo(Guid.Empty));
            Assert.That(await _db.ShelvedBooks.AnyAsync(x => x.Id == new ShelvedBookId(result.Model.Value)), Is.True);
        }

        [Test]
        public async Task Reshelves_old_ShelvedBook()
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

            var command = new ShelveBookRequest(book.Id.Value, shelf2.Id.Value, userId.Value);
            var result = await sut.Handle(command, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Model, Is.Not.EqualTo(Guid.Empty));
            Assert.That(await _db.ShelvedBooks.AnyAsync(x => x.Id == shelvedBook.Id && x.ShelfId == shelf2.Id));
            Assert.That(await _db.ShelvedBooks.CountAsync(), Is.EqualTo(1));
        }
    }
}
