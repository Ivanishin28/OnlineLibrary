using BookContext.Application.Configuration;
using BookContext.DL.Interfaces;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Tests.UseCases
{
    public class CreateAuthorTests
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public async Task SetUp()
        {
            IServiceCollection services = new ServiceCollection();
            services.RegisterBookContext(null);

            var provider = services.BuildServiceProvider();
            
            _authorRepository = provider.GetRequiredService<IAuthorRepository>();
            _unitOfWork = provider.GetRequiredService<IUnitOfWork>();
        }

        [Test]
        public async Task Should()
        {
            var authorCreationResult = Author
                .Create(
                    "FirstName",
                    "LastName",
                    DateOnly.FromDateTime(DateTime.Now));

            Assert.That(authorCreationResult.IsSuccess);
            Assert.That(authorCreationResult.Model is not null);

            var author = authorCreationResult.Model;
            await _authorRepository.Add(author);

            Assert.DoesNotThrowAsync(async () =>
            {
                await _unitOfWork.SaveChangesAsync();
            });
        }
    }
}
