using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Errors
{
    public static class LoginErrors
    {
        private static readonly ErrorBuilder builder = new ErrorBuilder("Identity.Login");

        public static readonly Error LOGIN_NOT_FOUND = builder.BuildError("Login.NOT_FOUND", "Login not found");
        public static readonly Error NOT_ALLOWED = builder.BuildError("Password.Wrong", "Wrong Password");
    }
}
