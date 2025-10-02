using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Errors
{
    public static class AccessibilityErrors
    {
        public static readonly Error CANNOT_ACCESS_RESOUCE = new Error("Accessibility", "Error");
    }
}
