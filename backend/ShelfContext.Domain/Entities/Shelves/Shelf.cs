using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Entities.Shelves
{
    public class Shelf
    {
        public ShelfId Id { get; private set; }
        public UserId UserId { get; private set; }
        public ShelfName Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        private Shelf() { }

        private Shelf(UserId userId, ShelfName name, DateTime dateCreated)
        {
            UserId = userId;
            Name = name;
            DateCreated = dateCreated;
        }

        public static Result<Shelf> Create(UserId clientId, ShelfName shelfName)
        {
            var dateCreated = DateTime.Now;

            return new Shelf(clientId, shelfName, dateCreated);
        }
    }
}
