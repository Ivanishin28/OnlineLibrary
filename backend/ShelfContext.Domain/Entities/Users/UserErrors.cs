using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Users
{
    public static class UserErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("User");

        public static readonly Error NotFound
            = _errors.BuildError("NotFound", "NotFound");
    }
}
