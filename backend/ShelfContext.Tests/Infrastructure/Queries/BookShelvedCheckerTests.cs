using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.Read;
using ShelfContext.DL.Read.Entities;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.Tests.Infrastructure.Queries
{
    public class BookShelvedCheckerTests
    {
        private ShelfReadDbContext _db;
        private IBookShelvedChecker _sut;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfReadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new ShelfReadDbContext(options);
            _sut = new BookShelvedChecker(_db);
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

            Assert.DoesNotThrowAsync(async () =>
            {
                await _sut.IsBookShelvedBy(bookId, userId);
            });
        }

        [Test]
        public async Task Handle_WithNonExistent_ReturnsFalse()
        {
            var bookId = new BookId(Guid.NewGuid());
            var userId = new UserId(Guid.NewGuid());

            var response = await _sut.IsBookShelvedBy(bookId, userId);
            
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

            var response = await _sut.IsBookShelvedBy(
                new BookId(bookFromDb.Id),
                new UserId(userFromDb.Id));

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

            var response = await _sut.IsBookShelvedBy(
                new BookId(bookFromDb.Id),
                new UserId(userFromDb.Id));

            Assert.False(response);
        }
    }
}
