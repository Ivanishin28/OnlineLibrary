﻿using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Errors
{
    public static class AuthorErrors
    {
        private static readonly ErrorBuilder _errors = new ErrorBuilder("Author");

        public static readonly Error FullNameError
            = _errors.BuildError("FullName", "FullName");
    }
}
