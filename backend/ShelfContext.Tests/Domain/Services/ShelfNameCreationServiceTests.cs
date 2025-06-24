using MediatR;
using Moq;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Services;

namespace ShelfContext.Tests.Domain.Services
{
    public class ShelfNameCreationServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        private ShelfNameCreationService _service;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorMock = new Mock<IMediator>();

            _service = new ShelfNameCreationService(
                _userRepositoryMock.Object,
                _mediatorMock.Object
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
            _mediatorMock.Setup(r =>
                r.Send(
                    It.IsAny<IsShelfNameTakenByUserQuery>(),
                    It.IsAny<CancellationToken>()))
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

            _mediatorMock
                .Setup(r =>
                    r.Send(
                        It.Is<IsShelfNameTakenByUserQuery>(query => 
                            query.ShelfName.Value == shelfName &&
                            query.UserId == userId),
                        It.IsAny<CancellationToken>()
                    ))
                .ReturnsAsync(false);

            var nameResult = await _service.Create(userId, shelfName);

            Assert.True(nameResult.IsSuccess);
            Assert.AreEqual(nameResult.Model.Value, shelfName);
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
