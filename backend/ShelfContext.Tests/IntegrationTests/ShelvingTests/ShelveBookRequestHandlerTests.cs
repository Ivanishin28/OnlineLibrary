using Microsoft.EntityFrameworkCore;
using Moq;
using ShelfContext.Contract.Services;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.Domain.Services;
using ShelfContext.UseCases.Commands;

namespace ShelfContext.Tests.IntegrationTests.ShelvingTests
{
    internal class ShelveBookRequestHandlerTests
    {
        private ShelveBookRequestHandler sut = null!;

        private ShelfDbContext _db = null!;
        private Mock<IResouceAccessibilityChecker> _checker = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            _checker = new Mock<IResouceAccessibilityChecker>();

            var service = new ShelvingService(
                new ShelfRepository(_db), 
                new BookAccessor(_db), 
                new ShelvedBookRepository(_db));

            sut = new ShelveBookRequestHandler(
                new UnitOfWork(_db),
                _checker.Object,
                service);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }
    }
}
