using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BooksOnShelves;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Entities.Shelves
{
    public class Shelf
    {
        private List<BookOnAShelf> _books;

        public ShelfId Id { get; private set; }
        public UserId UserId { get; private set; }
        public ShelfName Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        public IReadOnlyCollection<BookOnAShelf> Books => _books.AsReadOnly();

        private Shelf() { }

        private Shelf(UserId userId, ShelfName name, DateTime dateCreated)
        {
            UserId = userId;
            Name = name;
            DateCreated = dateCreated;
            _books = new();
        }

        public Result Shelve(Book book)
        {
            var isAlreadyShelved = _books
                .Any(book => 
                    book.BookId.Equals(book.Id));

            if(isAlreadyShelved)
            {
                return Result.Failure(ShelfErrors.BookAlreadyShelved);
            }

            var shelvedBook = BookOnAShelf.Create(Id, book.Id);
            _books.Add(shelvedBook);

            return Result.Success();
        }

        public static Result<Shelf> Create(UserId clientId, ShelfName shelfName)
        {
            var dateCreated = DateTime.Now;

            return new Shelf(clientId, shelfName, dateCreated);
        }
    }
}
