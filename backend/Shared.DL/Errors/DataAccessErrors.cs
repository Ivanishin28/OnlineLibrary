using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DL.Errors
{
    public static class DataAccessErrors
    {
        public static readonly Error NotFound = new Error("DL.NotFound", "DL.NotFound");
    }
}
