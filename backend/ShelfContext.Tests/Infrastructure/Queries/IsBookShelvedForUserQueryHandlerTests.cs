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

        [Test]
        public async Task Handle_WithNonExistent_ReturnsFalse()
        {
            var bookId = new BookId(Guid.NewGuid());
            var userId = new UserId(Guid.NewGuid());

            var request = new IsBookShelvedForUserQuery(bookId, userId);

            var response = await _handler.Handle(request, new CancellationToken());
            Assert.False(response);
        }

        [Test]
        public async Task Handle_WithExisting_ReturnsTrue()
        {
            var bookToAdd = new BookReadModel()
            {
                Id = Guid.NewGuid()
            };
            var userToAdd = new UserReadModel()
            {
                Id = Guid.NewGuid()
            };
            var shelfToAdd = new ShelfReadModel()
            {
                User = userToAdd,
                Name = "Name",
                DateCreated = DateTime.UtcNow
            };
            var shelvedBookToAdd = new ShelvedBookReadModel()
            {
                Shelf = shelfToAdd,
                Book = bookToAdd,
                DateShelved = DateTime.UtcNow
            };

            _db.ShelvedBooks.Add(shelvedBookToAdd);

            _db.SaveChanges();

            var bookFromDb = _db.Books.First(book => book.Id == bookToAdd.Id);
            var userFromDb = _db.Users.First(user => user.Id == userToAdd.Id);

            var request = new IsBookShelvedForUserQuery(
                new BookId(bookFromDb.Id), 
                new UserId(userFromDb.Id));
            var response = await _handler.Handle(request, new CancellationToken());

            Assert.True(response);
        }

        [Test]
        public async Task Handle_WithSameBookButDifferentUser_ReturnsFalse()
        {
            var bookToCheck = new BookReadModel()
            {
                Id = Guid.NewGuid()
            };
            var userWithBook = new UserReadModel()
            {
                Id = Guid.NewGuid()
            };
            var userWithoutBook = new UserReadModel()
            {
                Id = Guid.NewGuid()
            };
            var shelfWithBook = new ShelfReadModel()
            {
                User = userWithBook,
                Name = "Name",
                DateCreated = DateTime.UtcNow
            };
            var shelvedBookToAdd = new ShelvedBookReadModel()
            {
                Shelf = shelfWithBook,
                Book = bookToCheck,
                DateShelved = DateTime.UtcNow
            };

            _db.ShelvedBooks.Add(shelvedBookToAdd);
            _db.Users.Add(userWithoutBook);

            _db.SaveChanges();

            var bookFromDb = _db.Books.First(book => book.Id == bookToCheck.Id);
            var userFromDb = _db.Users.First(user => user.Id == userWithoutBook.Id);

            var request = new IsBookShelvedForUserQuery(
                new BookId(bookFromDb.Id),
                new UserId(userFromDb.Id));
            var response = await _handler.Handle(request, new CancellationToken());

            Assert.False(response);
        }
    }
}
