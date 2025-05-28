using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.SqlServer;
using ShelfContext.DL.SqlServer.Configuration;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.DL.SqlServer
{
    public class ShelvedBookRepositoryTests
    {
        private IServiceScope _scope;
        private IShelvedBookRepository _shelvedBookRepository;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services
                .RegisterDbContext()
                .RegisterRepositories();

            var provider = services.BuildServiceProvider();
            _scope = provider.CreateScope();

            _shelvedBookRepository = _scope.ServiceProvider.GetRequiredService<IShelvedBookRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        [Test]
        public async Task ShouldNotThrow()
        {
            var db = _scope.ServiceProvider.GetRequiredService<ShelfDbContext>();

            Assert.DoesNotThrow(() =>
            {
                var shelvedBooks = db.ShelvedBooks.Include(sb => sb.BookTags);
            });
        }
    }
}
