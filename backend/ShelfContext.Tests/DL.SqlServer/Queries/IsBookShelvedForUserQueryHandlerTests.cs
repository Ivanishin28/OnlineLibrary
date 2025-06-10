using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Application.Configuration;
using ShelfContext.DL.SqlServer;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.DL.SqlServer.Queries
{
    public class IsBookShelvedForUserQueryHandlerTests
    {
        private IMediator _mediator;

        [SetUp]
        public async Task SetUp()
        {
            IServiceCollection services = new ServiceCollection();
            services.RegisterShelfContext();

            var provider = services.BuildServiceProvider();

            _mediator = provider.GetRequiredService<IMediator>();
        }

        [Test]
        public async Task ShouldBeTrue()
        {
            var bookId = new BookId(new Guid("7475e205-2a82-4b2a-a576-f6ca88c052f6"));
            var userId = new UserId(new Guid("44fa5cd7-8b40-418b-0086-08dd8f1e200a"));
            var query = new IsBookShelvedForUserQuery(bookId, userId);

            var result = await _mediator.Send(query);

            Assert.That(result == true);
        }
    }
}
