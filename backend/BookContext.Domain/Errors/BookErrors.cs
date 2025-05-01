using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Errors
{
    public class BookErrors
    {
        public const string EMPTY_AUTHOR_LIST = "EMPTY_AUTHOR_LIST";
        public const string DUPLICATE_AUTHORS_ERROR = "DUPLICATE_AUTHORS_ERROR";
        public const string DIFFERENT_BOOK_AUTHOR_ERROR = "DIFFERENT_BOOK_AUTHOR_ERROR";
    }
}
