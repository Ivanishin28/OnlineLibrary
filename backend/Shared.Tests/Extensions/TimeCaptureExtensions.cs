using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tests.Extensions
{
    public static class TimeCaptureExtensions
    {
        public static DateTimeRange Capture(Action action)
        {
            var start = DateTime.Now;
            action();
            var end = DateTime.Now;

            return new DateTimeRange(start, end);
        }

        public static async Task<DateTimeRange> CaptureAsync(Func<Task> func)
        {
            var start = DateTime.Now;
            await func();
            var end = DateTime.Now;

            return new DateTimeRange(start, end);
        }
    }
}