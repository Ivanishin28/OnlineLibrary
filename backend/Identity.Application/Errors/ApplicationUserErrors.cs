using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Errors
{
    public static class ApplicationUserErrors
    {
        private static readonly ErrorBuilder builder = new ErrorBuilder("ApplicationUser");
        public static readonly Error UNIQUE_IDENTITY = builder.BuildError("Login", "Unique login and email");
    }
}
