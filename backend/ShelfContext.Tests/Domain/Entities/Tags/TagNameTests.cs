using Shared.Core.Extensions;
using ShelfContext.Domain.Entities.Tags;

namespace ShelfContext.Tests.Domain.Entities.Tags
{
    public class TagNameTests
    {
        [Test]
        public void Create_EmptyString_ReturnsFailure()
        {
            var name = String.Empty;

            var result = TagName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(TagErrors.EmptyName));
        }

        [Test]
        public void Create_LongString_ReturnsFailure()
        {
            var name = "Tag" + new string('g', TagName.MAX_LENGTH);

            var result = TagName.Create(name);

            Assert.That(result.IsFailure);
            Assert.That(result.HasError(TagErrors.TooLongName));
        }

        [Test]
        public void Create_RegularName_ReturnsSuccess()
        {
            var name = "Tagname";

            var result = TagName.Create(name);

            Assert.That(result.IsSuccess);
            Assert.That(result.Model.Value == name);
        }

        [Test]
        public void Equals_ForTwoEqual_ReturnsTrue()
        {
            var name1 = TagName.Create("Firsttag").Model;
            var name2 = TagName.Create("Firsttag").Model;

            Assert.That(name1.Equals(name2));
        }

        [Test]
        public void Equals_ForTwoDifferent_ReturnsFalse()
        {
            var name1 = TagName.Create("First").Model;
            var name2 = TagName.Create("Second").Model;

            Assert.IsFalse(name1.Equals(name2));
        }
    }
}
