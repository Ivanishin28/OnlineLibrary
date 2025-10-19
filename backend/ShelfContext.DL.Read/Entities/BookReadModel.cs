namespace ShelfContext.DL.Read.Entities
{
    public class BookReadModel : ReadModel
    {
        public Guid CreatorId { get; set; }

        public UserReadModel Creator { get; set; } = null!;
        public ICollection<ShelvedBookReadModel> ShelvedBooks { get; set; } = null!;
    }
}
