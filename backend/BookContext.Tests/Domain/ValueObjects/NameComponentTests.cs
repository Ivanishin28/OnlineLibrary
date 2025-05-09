using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookContext.Domain.Constants;
using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Extensions;
using Shared.Core.Models;

namespace BookContext.Tests.Domain.ValueObjects
{
    [TestFixture]
    public class NameComponentTests
    {
        [Test]
        public void ShouldAllSucceed()
        {
            var validNames = new string[]
            {
                "John",
                "O'Connor",
                "D'Artagnan",
                "McDonald",
                "O'Malley",
            };

            var creationResult = validNames.Select(name => NameComponent.Create(name)).ToArray();

            var combinedResult = ResultExtensions.Combine(creationResult);

            Assert.That(combinedResult.IsSuccess);
        }

        [Test]
        public void ShouldAllFail()
        {
            var invalidNames = new string[]
            {
                "john",
                "'Connor",
                "O''Connor",
                "O'connor",
                "John3",
                "John Doe",
            };

            var creationResult = invalidNames.Select(name => NameComponent.Create(name)).ToArray();

            Assert.That(creationResult.All(result => result.HasError(NameComponentErrors.PATTERN)));
        }

        [Test]
        public void ShouldFailWithEmptyName()
        {
            string? name = null;
            var creationResult = NameComponent.Create(name);

            Assert.That(creationResult.IsFailure);
            Assert.That(creationResult.HasError(NameComponentErrors.PATTERN));
        }

        [Test]
        public void ShouldFailWithTooLongName()
        {
            var name = "N" + new string('o', AuthorConstants.MAX_NAME_COMPONENT_LENGTH);
            var creationResult = NameComponent.Create(name);

            Assert.That(creationResult.IsFailure);
            Assert.That(creationResult.HasError(NameComponentErrors.LENGTH));
        }
    }
}
