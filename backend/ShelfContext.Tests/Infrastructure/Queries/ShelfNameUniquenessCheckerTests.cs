using Microsoft.EntityFrameworkCore;
using Shared.Tests.Exceptions;
using ShelfContext.DL.Read;
using ShelfContext.DL.Read.Entities;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.Tests.Infrastructure.Queries
{
    public class ShelfNameUniquenessCheckerTests
    {
        private ShelfReadDbContext _db;
        private IShelfNameUniquenessChecker _sut;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfReadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new ShelfReadDbContext(options);
            var handler = new IsShelfNameTakenByUserQueryHandler(_db);
            _sut = new ShelfNameUniquenessChecker(handler);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task IsShelfNameTaken_WithNotTaken_ReturnsFalse()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelfName = GetTestShelfName();

            var result = await _sut.IsNameTakenBy(shelfName, userId);

            Assert.False(result);
        }

        [Test]
        public async Task IsShelfNameTaken_WithTaken_ReturnsTrue()
        {
            var shelfName = GetTestShelfName();

            var user = new UserReadModel()
            {
                Id = Guid.NewGuid()
            };
            var shelf = new ShelfReadModel()
            {
                User = user,
                Name = shelfName.Value,
                DateCreated = DateTime.UtcNow
            };

            _db.Add(user);
            _db.Add(shelf);

            _db.SaveChanges();

            var result = await _sut.IsNameTakenBy(shelfName, new UserId(user.Id));

            Assert.True(result);
        }

        [Test]
        public async Task IsShelfNameTaken_WithNameTakenByAnotherUser_ReturnsFalse()
        {
            var shelfName = GetTestShelfName();
            var userWithTakenName = new UserReadModel
            {
                Id = Guid.NewGuid(),
                Shelves = new[]
                {
                    new ShelfReadModel()
                    {
                        Id = Guid.NewGuid(),
                        Name = shelfName.Value,
                        DateCreated = DateTime.UtcNow,
                    }
                }
            };
            var userWithoutTakenName = new UserReadModel
            {
                Id = Guid.NewGuid()
            };

            _db.Users.AddRange(userWithTakenName, userWithoutTakenName);

            _db.SaveChanges();

            var result = await _sut.IsNameTakenBy(
                shelfName,
                new UserId(userWithoutTakenName.Id));

            Assert.False(result);
        }

        private ShelfName GetTestShelfName(string ending = "")
        {
            var result = ShelfName.Create($"Shelf{ending}");

            if(result.IsFailure)
            {
                throw new ArrangeTestException();
            }

            return result.Model;
        }
    }
}
