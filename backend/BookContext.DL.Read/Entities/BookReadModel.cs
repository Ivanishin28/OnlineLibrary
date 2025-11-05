using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Entities
{
    public class BookReadModel
    {
        public Guid Id { get; private set; }
        public Guid CreatorId { get; private set; }
        public string Title { get; private set; } = null!;
        public IReadOnlyCollection<BookAuthorReadModel> BookAuthors { get; private set; } 
            = new List<BookAuthorReadModel>();
        public IReadOnlyCollection<BookGenreReadModel> BookGenres { get; private set; }
            = new List<BookGenreReadModel>();
        public BookMetadataReadModel BookMetadata { get; init; } = null!;
    }
}
