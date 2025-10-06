using System.Text.Json.Serialization;

namespace ShelfContext.Contract.Dtos
{
    public record ShelvedBookDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; private set; }
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }
        [JsonPropertyName("shelf_id")]
        public Guid ShelfId { get; private set; }
        [JsonPropertyName("date_shelved")]
        public DateTime DateShelved { get; private set; }
        [JsonPropertyName("tags")]
        public ICollection<BookTagDto> Tags { get; private set; }

        public ShelvedBookDto(Guid id, Guid bookId, Guid shelfId, DateTime dateShelved, ICollection<BookTagDto> tags)
        {
            Id = id;
            BookId = bookId;
            ShelfId = shelfId;
            DateShelved = dateShelved;
            Tags = tags;
        }
    }
}
