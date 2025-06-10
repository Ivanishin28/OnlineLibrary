using Shared.Core.Models;
using Shared.Tests.Extensions;
using ShelfContext.Domain.Entities.Tags;

namespace ShelfContext.Tests.Domain.Entities.Tags
{
    public class TagTests
    {
        [Test]
        public void Create_TagName_ReturnsSuccess()
        {
            var name = "Tagname";
            var tagNameResult = TagName.Create(name);

            Assert.That(tagNameResult.IsSuccess);

            Result<Tag> tagResult = null;

            var range = TimeCaptureExtensions
                .Capture(
                    () => tagResult = Tag.Create(tagNameResult.Model)
                );

            Assert.That(tagResult.IsSuccess);
            AssertExtensions.AssertInRange(range, tagResult.Model.DateCreated);
        }
    }
}
