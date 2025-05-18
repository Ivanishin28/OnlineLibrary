using Shared.Core.Models;
using Shared.Core.ValueObjects;
using ShelfContext.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Shelves
{
    public class ShelfName : ValueObject
    {
        public const int MAX_LENGTH = 32;

        public string Value { get; private set; }

        private ShelfName(string value)
        {
            Value = value;
        }

        public static Result<ShelfName> Create(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return Result<ShelfName>.Failure(ShelfErrors.EmptyShelfName);
            }

            if(name.Length > MAX_LENGTH)
            {
                return Result<ShelfName>.Failure(ShelfErrors.LongShelfName);
            }

            return new ShelfName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new object[] { Value };
        }
    }
}
