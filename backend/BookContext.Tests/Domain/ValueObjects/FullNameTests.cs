using BookContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Tests.Domain.ValueObjects
{
    public class FullNameTests
    {
        [Test]
        public void ShouldCreateFullNameWithoutMiddlename()
        {
            var firstName = "Name";
            var lastName = "Surname";
            string? middleName = null;

            var creationResult = FullName.Create(firstName, lastName, middleName);

            Assert.That(creationResult.IsSuccess);
        }

        [Test]
        public void ShouldCreateFullNameWithMiddlename()
        {
            var firstName = "Name";
            var lastName = "Surname";
            var middleName = "MiddleName";

            var creationResult = FullName.Create(firstName, lastName, middleName);

            Assert.That(creationResult.IsSuccess);
        }
    }
}
