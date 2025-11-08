using System;

namespace BookContext.DL.Read.Entities
{
    public class GenreReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;

        public IReadOnlyCollection<BookGenreReadModel> BookGenres { get; init; }
            = new List<BookGenreReadModel>();
    }
}

