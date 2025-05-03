using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.ValueConverters
{
    public class NameComponentValueConverter : ValueConverter<NameComponent, string>
    {
        public NameComponentValueConverter() 
            : base(
                  (valueObject) => FromValueObject(valueObject),
                  (primitive) => FromPrimitive(primitive)) { }

        public static NameComponent FromPrimitive(string value)
        {
            var result = NameComponent.Create(value);

            if(result.IsFailure)
            {
                throw new ValueConvertionException();
            }

            return result.Model;
        }

        public static string FromValueObject(NameComponent component)
        {
            return component.Value;
        }
    }
}
