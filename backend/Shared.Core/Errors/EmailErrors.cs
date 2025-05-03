using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Errors
{
    public static class EmailErrors
    {
        public static readonly Error InvalidEmail = new Error("Email.Invalid", "Email.Invalid");
    }
}
