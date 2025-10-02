namespace ShelfContext.Domain.Entities.Books
{
    public class Book
    {
        public BookId Id { get; private set; } = null!;

        private Book() { }
    }
}
