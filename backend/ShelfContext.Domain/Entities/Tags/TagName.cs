using Shared.Core.Models;
using Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Tags
{
    public class TagName : ValueObject
    {
        public const int MAX_LENGTH = 32;
        
        public string Value { get; private set; }

        private TagName(string value)
        {
            Value = value;
        }

        public static Result<TagName> Create(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return Result<TagName>.Failure(TagErrors.EmptyName);
            }

            if(name.Length > MAX_LENGTH)
            {
                return Result<TagName>.Failure(TagErrors.TooLongName);
            }

            return new TagName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new[] { Value };
        }
    }
}
