using Shared.Core.Extensions;
using ShelfContext.Domain.Entities.Tags;

namespace ShelfContext.Tests.Domain.Entities.Tags
{
    public class TagNameTests
    {
        [Test]
        public void Create_EmptyString_ShouldReturnFailure()
        {
            var name = String.Empty;

            var result = TagName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(TagErrors.EmptyName));
        }

        [Test]
        public void Create_LongString_ShouldReturnFailure()
        {
            var name = "Tag" + new string('g', TagName.MAX_LENGTH);

            var result = TagName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(TagErrors.TooLongName));
        }

        [Test]
        public void Create_RegularName_ShouldReturnSuccess()
        {
            var name = "Tagname";

            var result = TagName.Create(name);

            Assert.That(result.IsSuccess);
            Assert.That(result.Model.Value == name);
        }

        [Test]
        public void Equals_ForTwoEqual_ShouldReturnTrue()
        {
            var name1 = TagName.Create("Firsttag").Model;
            var name2 = TagName.Create("Firsttag").Model;

            Assert.That(name1.Equals(name2));
        }

        [Test]
        public void Equals_ForTwoDifferent_ShouldReturnFalse()
        {
            var name1 = TagName.Create("First").Model;
            var name2 = TagName.Create("Second").Model;

            Assert.IsFalse(name1.Equals(name2));
        }
    }
}
