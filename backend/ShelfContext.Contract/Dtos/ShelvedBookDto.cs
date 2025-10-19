using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Dtos
{
    public record ShelvedBookDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; private set; }
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }
        [JsonPropertyName("date_shelved")]
        public DateTime DateShelved { get; private set; }
        [JsonPropertyName("shelf")]
        public ShelfDto Shelf { get; private set; }
        [JsonPropertyName("tags")]
        public ICollection<BookTagDto> Tags { get; private set; }

        public ShelvedBookDto(Guid id, Guid bookId, ShelfDto shelf, DateTime dateShelved, ICollection<BookTagDto> tags)
        {
            Id = id;
            BookId = bookId;
            Shelf = shelf;
            DateShelved = dateShelved;
            Tags = tags;
        }
    }
}
