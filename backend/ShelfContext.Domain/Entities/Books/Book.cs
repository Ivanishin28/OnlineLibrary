using ShelfContext.Domain.Entities.BookTags;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Books
{
    public class Book
    {
        public BookId Id { get; private set; }

        private Book() { }
    }
}
