using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Entities
{
    public class AuthorReadModel
    {
        public Guid Id { get; init; }
        public Guid CreatorId { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? MiddleName { get; init; }

        public IReadOnlyCollection<BookAuthorReadModel> BookAuthors { get; init; }
            = new List<BookAuthorReadModel>();
        public AuthorMetadataReadModel AuthorMetadata { get; init; } = null!;
    }
}
