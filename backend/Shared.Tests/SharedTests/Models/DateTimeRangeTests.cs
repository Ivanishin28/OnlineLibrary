using Shared.Core.Models;
using Shared.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tests.SharedTests.Models
{
    public class DateTimeRangeTests
    {
        [Test]
        public void Contains_BetweenStartAndEnd_ReturnsTrue()
        {
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);

            var toTest = DateTime.Now.AddSeconds(5);

            var dateRange = new DateTimeRange(start, end);
            AssertExtensions.AssertInRange(dateRange, toTest);
        }

        [Test]
        public void Contains_Start_ReturnsTrue()
        {
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);

            var toTest = start;

            var dateRange = new DateTimeRange(start, end);
            AssertExtensions.AssertInRange(dateRange, toTest);
        }

        [Test]
        public void Contains_End_ReturnsTrue()
        {
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);

            var toTest = end;

            var dateRange = new DateTimeRange(start, end);
            AssertExtensions.AssertInRange(dateRange, toTest);
        }

        [Test]
        public void Constructor_StartBeforeEnd_ThrowsArgumentException()
        {
            var start = DateTime.Now.AddSeconds(10);
            var end = DateTime.Now;

            Assert.Throws<ArgumentException>(() =>
            {
                var dateRange = new DateTimeRange(start, end);
            });
        }
    }
}
