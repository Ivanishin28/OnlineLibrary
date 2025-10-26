using BookContext.Contract.Commands;
using BookContext.Contract.Events;
using BookContext.DL.SqlServer;
using BookContext.DL.SqlServer.Concrete;
using BookContext.DL.SqlServer.Repositories;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using BookContext.UseCases.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Tests.Integration.BookTests
{
    public class DeleteBookRequestHandlerTests
    {
        private BookDbContext _db = null!;
        private Mock<IMediator> _mediator = null!;

        private DeleteBookRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new BookDbContext(options);

            _mediator = new Mock<IMediator>();

            sut = new DeleteBookRequestHandler(
                new BookRepository(_db),
                new UnitOfWork(_db),
                _mediator.Object);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Deletes_book_and_emits_delete_book_event()
        {
            var book = Book.Create(new UserId(Guid.NewGuid()), "Title").Model;
            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            var request = new DeleteBookRequest(book.Id.Value);
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            _mediator.Verify(x =>
                x.Publish(
                    It.Is<BookDeletedEvent>(e => e.BookId == book.Id.Value),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
