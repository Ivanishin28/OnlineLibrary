using Shared.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tests.SharedTests.Extensions
{
    public class TimeCaptureExtensionsTests
    {
        [Test]
        public void Capture_DateTimeNow_ReturnsDateTimeRange()
        {
            DateTime? now = null;

            var range = TimeCaptureExtensions.Capture(() =>
            {
                now = DateTime.Now;
            });

            Assert.NotNull(now);
            AssertExtensions.AssertInRange(range, now.Value);
        }
    }
}
