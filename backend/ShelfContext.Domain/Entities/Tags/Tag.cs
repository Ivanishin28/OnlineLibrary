using Shared.Core.Models;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Tags
{
    public class Tag
    {
        public TagId Id { get; private set; } = null!;
        public UserId UserId { get; set; } = null!;
        public TagName Name { get; private set; } = null!;
        public DateTime DateCreated { get; private set; }

        private Tag() { }

        private Tag(TagId id, TagName name, DateTime dateCreated, UserId userId)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
            UserId = userId;
        }

        public static Result<Tag> Create(UserId userId, TagName name)
        {
            var id = new TagId(Guid.NewGuid());
            var dateCreated = DateTime.Now;

            return new Tag(id, name, dateCreated, userId);
        }

        public void UpdateName(TagName name)
        {
            Name = name;
        }
    }
}
