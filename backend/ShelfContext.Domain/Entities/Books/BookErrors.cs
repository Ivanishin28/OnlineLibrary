using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Books
{
    public static class BookErrors
    {
        private static ErrorBuilder _errors = new ErrorBuilder("Book");
    }
}
