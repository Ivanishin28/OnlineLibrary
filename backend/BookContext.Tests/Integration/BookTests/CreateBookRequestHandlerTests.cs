using BookContext.Contract.Commands.CreateBook;
using BookContext.DL.SqlServer;
using BookContext.DL.SqlServer.Concrete;
using BookContext.DL.SqlServer.Repositories;
using BookContext.UseCases.Commands;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookContext.Tests.Integration.BookTests
{
    internal class CreateBookRequestHandlerTests
    {
        private BookDbContext _db = null!;

        private CreateBookRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new BookDbContext(options);

            sut = new CreateBookRequestHandler(
                new BookRepository(_db),
                new BookMetadataRepository(_db),
                new UnitOfWork(_db),
                new AuthorRepository(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Should_create_book()
        {
            var request = new CreateBookRequest()
            {
                CreatorId = Guid.NewGuid(),
                Title = "Test Book",
                PublishingDate = DateOnly.FromDateTime(DateTime.Now)
            };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(await _db.Books.CountAsync(), Is.EqualTo(1));
            var book = await _db.Books.FirstAsync();
            Assert.That(book.CreatorId, Is.EqualTo(new UserId(request.CreatorId)));
            Assert.That(book.Title, Is.EqualTo(request.Title));
        }

        [Test]
        public async Task Should_create_metadata_without_optional()
        {
            var request = new CreateBookRequest()
            {
                CreatorId = Guid.NewGuid(),
                Title = "Test Book",
                PublishingDate = DateOnly.FromDateTime(DateTime.Now)
            };

            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(await _db.BookMetadatas.CountAsync(), Is.EqualTo(1));
            var book = await _db.Books.FirstAsync();
            var metadata = await _db.BookMetadatas.FirstAsync();
            Assert.That(metadata.BookId, Is.EqualTo(book.Id));
            Assert.That(metadata.Description, Is.Not.Null);
            Assert.That(metadata.Description.Value, Is.Null.Or.Empty);
            Assert.That(metadata.CoverId, Is.Null);
        }

        [Test]
        public async Task Should_create_metadata_with_optional()
        {
            var coverId = Guid.NewGuid();
            var request = new CreateBookRequest()
            {
                CreatorId = Guid.NewGuid(),
                Title = "Test Book",
                PublishingDate = DateOnly.FromDateTime(DateTime.Now),
                CoverId = coverId,
                Description = "This is a test book description"
            };

            var result = await sut.Handle(request, CancellationToken.None);

            var metadata = await _db.BookMetadatas.FirstAsync();
            Assert.That(metadata.Description, Is.Not.Null);
            Assert.That(metadata.CoverId, Is.Not.Null);
            Assert.That(metadata.CoverId.Value, Is.EqualTo(coverId));
        }
    }
} 