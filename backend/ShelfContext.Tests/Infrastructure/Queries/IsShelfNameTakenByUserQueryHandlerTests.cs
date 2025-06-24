using Microsoft.EntityFrameworkCore;
using Shared.Tests.Exceptions;
using ShelfContext.DL.Read;
using ShelfContext.DL.Read.Entities;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.Infrastructure.Queries
{
    public class IsShelfNameTakenByUserQueryHandlerTests
    {
        private ShelfReadDbContext _db;
        private IsShelfNameTakenByUserQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfReadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new ShelfReadDbContext(options);
            _handler = new IsShelfNameTakenByUserQueryHandler(_db);
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

            var query = new IsShelfNameTakenByUserQuery(shelfName, userId);

            var result = await _handler.Handle(query, new CancellationToken());

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

            var query = new IsShelfNameTakenByUserQuery(shelfName, new UserId(user.Id));

            var result = await _handler.Handle(query, new CancellationToken());

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

            var query = new IsShelfNameTakenByUserQuery(
                shelfName,
                new UserId(userWithoutTakenName.Id));

            var result = await _handler.Handle(query, new CancellationToken());

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
