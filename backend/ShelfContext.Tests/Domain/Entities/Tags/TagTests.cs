using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var tagResult = Tag.Create(tagNameResult.Model);

            Assert.That(tagResult.IsSuccess);
        }
    }
}
