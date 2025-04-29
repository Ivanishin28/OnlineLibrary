using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.ValueConverters
{
    public class AuthorsOfABookValueConverter : ValueConverter<AuthorsOfABook, ICollection<BookAuthor>>
    {
        public AuthorsOfABookValueConverter() : base(
            (booksCollection) => Create(booksCollection),
            (collection) => Create(collection))
        {

        }

        private static AuthorsOfABook Create(ICollection<BookAuthor> bookAuthors)
        {
            var result = AuthorsOfABook.Create(bookAuthors);

            if(result.IsFailure)
            {
                throw new ValueConvertionException();
            }

            return result.Model;
        }

        private static ICollection<BookAuthor> Create(AuthorsOfABook collection)
        {
            return collection
                .BookAuthors
                .ToList();
        }
    }
}
