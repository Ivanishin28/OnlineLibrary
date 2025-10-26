using BookContext.DL.SqlServer.Repositories;
using BookContext.DL.SqlServer;
using BookContext.UseCases.Commands;
using Microsoft.EntityFrameworkCore;
using BookContext.DL.SqlServer.Concrete;
using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Domain.ValueObjects;

namespace BookContext.Tests.Integration.AuthorTests
{
    internal class CreateAuthorRequestHandlerTests
    {
        private BookDbContext _db = null!;

        private CreateAuthorRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new BookDbContext(options);

            sut = new CreateAuthorRequestHandler(
                new AuthorRepository(_db),
                new UnitOfWork(_db),
                new AuthorMetadataRepository(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Should_create_author()
        {
            var request = new CreateAuthorRequest()
            {
                CreatorId = Guid.NewGuid(),
                FirstName = "Firstname",
                LastName = "Lastname",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
            };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(await _db.Authors.CountAsync(), Is.EqualTo(1));
            var author = await _db.Authors.FirstAsync();
            Assert.That(author.CreatorId, Is.EqualTo(new UserId(request.CreatorId)));
        }

        [Test]
        public async Task Should_create_metadata_without_optional()
        {
            var request = new CreateAuthorRequest()
            {
                CreatorId = Guid.NewGuid(),
                FirstName = "Firstname",
                LastName = "Lastname",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
            };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(await _db.AuthorMetadatas.CountAsync(), Is.EqualTo(1));
            var author = await _db.Authors.FirstAsync();
            var metadata = await _db.AuthorMetadatas.FirstAsync();
            Assert.That(metadata.AuthorId, Is.EqualTo(author.Id));
            Assert.That(metadata.Biography, Is.Not.Null);
            Assert.That(metadata.Biography.Value, Is.Null.Or.Empty);
            Assert.That(metadata.AvatarId, Is.Null);
        }

        [Test]
        public async Task Should_create_metadata_with_optional()
        {
            var request = new CreateAuthorRequest()
            {
                CreatorId = Guid.NewGuid(),
                FirstName = "Firstname",
                LastName = "Lastname",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                AvatarId = Guid.NewGuid(),
                Biography = "Biography very interesting"
            };

            var result = await sut.Handle(request, CancellationToken.None);

            var metadata = await _db.AuthorMetadatas.FirstAsync();
            Assert.That(metadata.Biography, Is.Not.Null);
            Assert.That(metadata.AvatarId, Is.Not.Null);
        }
    }
}
