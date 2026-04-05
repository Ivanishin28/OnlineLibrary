using BookContext.DL.SqlServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookContext.Tests.Integration.AuthorTests;

internal class DeleteAuthorRequestHandlerTests
{
    private BookDbContext _db = null!;

    [SetUp]
    public async Task SetUp()
    {
        _db = new BookDbContext(
            TestContainerSetupFixture.GetContextOptions(), 
            new Mock<IPublisher>().Object);

        await TestContainerSetupFixture.Clear(_db);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _db.DisposeAsync();
    }

    [Test]
    public async Task Should()
    {
        var result = await _db.Books.AnyAsync();
        Assert.That(result, Is.False);
    }
}
