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
        private List<Shelf> _shelves = new();
        public UserId Id { get; private set; }

        public IImmutableList<Shelf> Shelves => _shelves.ToImmutableList();

        private User() { }


    }
}
