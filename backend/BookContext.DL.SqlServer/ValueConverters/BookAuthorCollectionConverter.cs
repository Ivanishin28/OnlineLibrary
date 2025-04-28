using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.ValueConverters
{
    public class BookAuthorCollectionConverter : ValueConverter<BooksAuthorCollection, ICollection<BookAuthor>>
    {
        public BookAuthorCollectionConverter() : base((collection) => null, (collection) => null)
        {

        }
    }
}
