using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Services;
using ShelfContext.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Should()
        {
            var user = new User()
            {
                Id = new UserId(Guid.NewGuid())
            };
            var shelf = Shelf.Create(user.Id, ShelfName.Create("Shelf").Model).Model;
            var book = new Book()
            {
                Id = new BookId(Guid.NewGuid())
            };
            _db.Add(user);
            _db.Add(shelf);
            _db.Add(book);
            await _db.SaveChangesAsync();

            var res = await _db.Shelves.FirstAsync();
            Assert.That(res, Is.Not.Null);
        }
    }
}
