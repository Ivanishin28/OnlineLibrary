using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Extensions
{
    public static class GuidExtensions
    {
        public static bool AllUnique(this ICollection<Guid> guids)
        {
            var unique = guids.Distinct();

            return unique.Count() < guids.Count();
        }
    }
}
