using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Entities.Shelves
{
    public class Shelf
    {
        public ShelfId Id { get; private set; } = null!;
        public UserId UserId { get; private set; } = null!;
        public ShelfName Name { get; private set; } = null!;
        public DateTime DateCreated { get; private set; }

        private Shelf() { }

        private Shelf(ShelfId id, UserId userId, ShelfName name, DateTime dateCreated)
        {
            Id = id;
            UserId = userId;
            Name = name;
            DateCreated = dateCreated;
        }

        public void UpdateName(ShelfName name)
        {
            Name = name;
        }

        public static Result<Shelf> Create(UserId clientId, ShelfName shelfName)
        {
            var dateCreated = DateTime.Now;
            var id = new ShelfId(Guid.NewGuid());

            return new Shelf(id, clientId, shelfName, dateCreated);
        }
    }
}
