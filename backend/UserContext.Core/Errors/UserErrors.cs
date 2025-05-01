using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Domain.Errors
{
    public static class UserErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("User");

        public static readonly Error FirstNameError
            = _errors.BuildError("FirstName.Ivalid", "Firstname.Ivalid");

        public static readonly Error LastNameError
            = _errors.BuildError("LastName.Ivalid", "LastName.Ivalid");
    }
}
