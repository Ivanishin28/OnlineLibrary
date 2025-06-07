using Shared.Core.Extensions;
using ShelfContext.Domain.Entities.Shelves;

namespace ShelfContext.Tests.Domain.Entities.Shelves
{
    public class ShelfNameTests
    {
        [Test]
        public void Create_EmptyString_ShouldReturnFailure()
        {
            var name = String.Empty;

            var result = ShelfName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(ShelfErrors.EmptyShelfName));
        }

        [Test]
        public void Create_LongString_ShouldReturnFailure()
        {
            var name = "Tag" + new string('g', ShelfName.MAX_LENGTH);

            var result = ShelfName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(ShelfErrors.LongShelfName));
        }

        [Test]
        public void Create_RegularName_ShouldReturnSuccess()
        {
            var name = "First";

            var result = ShelfName.Create(name);

            Assert.That(result.IsSuccess);
            Assert.That(result.Model.Value == name);
        }

        [Test]
        public void Equals_ForTwoEqual_ShouldReturnTrue()
        {
            var name1 = ShelfName.Create("First").Model;
            var name2 = ShelfName.Create("First").Model;

            Assert.That(name1.Equals(name2));
        }

        [Test]
        public void Equals_ForTwoDifferent_ShouldReturnFalse()
        {
            var name1 = ShelfName.Create("First").Model;
            var name2 = ShelfName.Create("Second").Model;

            Assert.IsFalse(name1.Equals(name2));
        }
    }
}
