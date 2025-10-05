using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Users
{
    public class User
    {
        public UserId Id { get; init; } = null!;

        public User() { }
    }
}
