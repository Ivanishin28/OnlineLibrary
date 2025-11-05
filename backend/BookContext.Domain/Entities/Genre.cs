using BookContext.Domain.ValueObjects;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Entities
{
    public class Genre
    {
        public GenreId Id { get; private set; } = null!;
        public string Name { get; private set; } = null!;

        private Genre() { }

        private Genre(GenreId id, string name)
        {
            Id = id;
            Name = name;
        }

        public Result<Genre> Create(string name)
        {
            return new Genre(new GenreId(Guid.NewGuid()), name);
        }
    }
}
