using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.DL.SqlServer;
using ShelfContext.Domain.Services;
using ShelfContext.Tests.Fakes;
using ShelfContext.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShelfContext.UseCases.EventHandlers;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Entities.Books;
using BookContext.Contract.Events;

namespace ShelfContext.Tests.IntegrationTests.BookDeletedTests
{
    internal class BookDeletedEventHandlerTests
    {
        private ShelfDbContext _db = null!;
        
        private BookDeletedEventHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            sut = new BookDeletedEventHandler(
                new UnitOfWork(_db),
                new ShelvedBookRepository(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Deletes_shelved_books_of_all_users()
        {
            var bookId = new BookId(Guid.NewGuid());
            var u1 = new UserId(Guid.NewGuid());
            var u2 = new UserId(Guid.NewGuid());
            var s1 = Shelf.Create(u1, ShelfName.Create("Users1").Model).Model;
            var s2 = Shelf.Create(u1, ShelfName.Create("Users2").Model).Model;
            var sb1 = ShelvedBook.Create(s1.Id, bookId, u1);
            var sb2 = ShelvedBook.Create(s2.Id, bookId, u2);
            _db.AddRange(s1, s2, sb1, sb2);
            await _db.SaveChangesAsync();

            var notification = new BookDeletedEvent(bookId.Value);
            await sut.Handle(notification, CancellationToken.None);

            Assert.That(await _db.ShelvedBooks.CountAsync(), Is.Zero);
        }
    }
}
