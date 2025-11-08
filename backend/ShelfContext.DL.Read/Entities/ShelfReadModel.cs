namespace ShelfContext.DL.Read.Entities
{
    public class ShelfReadModel : ReadModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }

        public ICollection<ShelvedBookReadModel> ShelvedBooks { get; set; } = null!;
    }
}
