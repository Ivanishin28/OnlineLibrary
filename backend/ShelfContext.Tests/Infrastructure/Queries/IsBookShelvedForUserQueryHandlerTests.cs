using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.Read;
using ShelfContext.DL.Read.Entities;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;

namespace ShelfContext.Tests.Infrastructure.Queries
{
    public class IsBookShelvedForUserQueryHandlerTests
    {
        private ShelfReadDbContext _db;
        private IsBookShelvedForUserQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfReadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new ShelfReadDbContext(options);
            _handler = new IsBookShelvedForUserQueryHandler(_db);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task Handle_WithAny_DoesntThrow()
        {
            var bookId = new BookId(Guid.NewGuid());
            var userId = new UserId(Guid.NewGuid());
            var request = new IsBookShelvedForUserQuery(bookId, userId);

            Assert.DoesNotThrowAsync(async () =>
            {
                await _handler.Handle(request, new CancellationToken());
            });
        }
    }
}
