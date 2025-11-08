using BookContext.Contract.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShelfContext.Contract.Commands.DislodgeBook;
using ShelfContext.Contract.Events;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.IntegrationTests.ShelvingTests
{
    public class DislodgeBookRequestHandlerTests
    {
        private ShelfDbContext _db = null!;
        private Mock<IMediator> _mediator = null!;

        private DislodgeBookRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            _mediator = new Mock<IMediator>();
            sut = new DislodgeBookRequestHandler(
                new UnitOfWork(_db),
                new ShelvedBookRepository(_db),
                _mediator.Object);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Deletes_shelved_book_and_emits_event()
        {
            var userId = new UserId(Guid.NewGuid());
            var bookId = new BookId(Guid.NewGuid());
            var shelf = Shelf.Create(userId, ShelfName.Create("name").Model).Model;
            var shelvedBook = ShelvedBook.Create(shelf.Id, bookId, userId);
            _db.AddRange(shelf, shelvedBook);
            await _db.SaveChangesAsync();

            var request = new DislodgeBookRequest(shelvedBook.Id.Value);
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(await _db.ShelvedBooks.AnyAsync(x => x.Id == shelvedBook.Id), Is.False);
            _mediator.Verify(x =>
                x.Publish(
                    It.Is<BookDislodgedEvent>(e => 
                        e.BookId == bookId.Value &&
                        e.UserId == userId.Value &&
                        e.ShelvedBookId == shelvedBook.Id.Value),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
