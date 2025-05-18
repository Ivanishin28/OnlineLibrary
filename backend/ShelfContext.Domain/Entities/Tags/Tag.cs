using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Tags
{
    public class Tag
    {
        public TagId Id { get; private set; }
        public TagName Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        private Tag() { }

        private Tag(TagId id, TagName name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }

        public static Result<Tag> Create(TagName name)
        {
            var id = new TagId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new Tag(id, name, dateCreated);
        }
    }
}
