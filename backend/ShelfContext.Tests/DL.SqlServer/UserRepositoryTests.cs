using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Application.Configuration;
using ShelfContext.DL.SqlServer;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.DL.SqlServer
{
    public class UserRepositoryTests
    {
        private ShelfDbContext _db;

        [SetUp]
        public async Task SetUp()
        {
            IServiceCollection services = new ServiceCollection();
            services.RegisterShelfContext();

            var provider = services.BuildServiceProvider();

            _db = provider.GetRequiredService<ShelfDbContext>();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task ShouldAccessUsersView()
        {
            Assert.DoesNotThrow(() =>
            {
                var user = _db.Users.FirstOrDefault();
            });
        }
    }
}
