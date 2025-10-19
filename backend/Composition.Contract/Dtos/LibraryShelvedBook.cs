using BookContext.Contract.Dtos;
using ShelfContext.Contract.Dtos;

namespace Composition.Contract.Dtos
{
    public record LibraryShelvedBook : ShelvedBookDto
    {
        public BookPreviewDto Book { get; init; }
        public LibraryShelvedBook(ShelvedBookDto original, BookPreviewDto book) : base(original)
        {
            Book = book;
        }
    }
}
