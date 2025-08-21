using MediatR;
using Moq;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Services;

namespace ShelfContext.Tests.Domain.Services
{
    public class ShelfNameCreationServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IShelfNameUniquenessChecker> _nameChecker;

        private ShelfNameCreationService _service;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _nameChecker = new Mock<IShelfNameUniquenessChecker>();

            _service = new ShelfNameCreationService(
                _userRepositoryMock.Object,
                _nameChecker.Object
            );
        }

        [Test]
        public async Task Create_WithNonExistentUserId_ReturnsFailure()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelf = GetValidShelfName();

            _userRepositoryMock.Setup(r => r.Exists(userId)).ReturnsAsync(false);

            var result = await _service.Create(userId, shelf);

            Assert.True(result.IsFailure);
        }

        [Test]
        public async Task Create_WithInvalidShelfName_ReturnsFailure()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelf = GetInvalidShelfName();

            _userRepositoryMock.Setup(r => r.Exists(userId)).ReturnsAsync(true);

            var result = await _service.Create(userId, shelf);

            Assert.True(result.IsFailure);
        }

        [Test]
        public async Task Create_WithDuplicateName_ReturnsFailure()
        {
            var userId = new UserId(Guid.NewGuid());
            var shelfName = GetValidShelfName();

            _userRepositoryMock.Setup(r => r.Exists(userId)).ReturnsAsync(true);
            _nameChecker.Setup(r => 
                r.IsNameTakenBy(
                    It.Is<ShelfName>(shelf => shelf.Value == shelfName),
                    userId))
                .ReturnsAsync(true);

            var result = await _service.Create(userId, shelfName);

            Assert.True(result.IsFailure);
        }

        [Test]
        public async Task Create_WithValidName_ReturnsShelfName()
        {
            var shelfName = GetValidShelfName();
            var userId = new UserId(Guid.NewGuid());

            _userRepositoryMock
                .Setup(r => 
                    r.Exists(It.IsAny<UserId>()))
                .ReturnsAsync(true);

            _nameChecker.Setup(r =>
                r.IsNameTakenBy(
                    It.Is<ShelfName>(shelf => shelf.Value == shelfName),
                    userId))
                .ReturnsAsync(false);

            var nameResult = await _service.Create(userId, shelfName);

            Assert.True(nameResult.IsSuccess);
            Assert.That(nameResult.Model.Value, Is.EqualTo(shelfName));
        }

        private string GetValidShelfName(string nameEnding = "")
        {
            return $"Shelf{nameEnding}";
        }

        private string GetInvalidShelfName()
        {
            return "";
        }
    }
}
