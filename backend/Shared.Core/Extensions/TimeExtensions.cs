using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Extensions
{
    public static class TimeExtensions
    {
        public static DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public static DateOnly Today()
        {
            return DateOnly.FromDateTime(Now());
        }
    }
}
