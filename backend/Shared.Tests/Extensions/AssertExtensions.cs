using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void AssertInRange(DateTimeRange range, DateTime dateTime)
        {
            Assert.That(range.Contains(dateTime));
        }
    }
}
