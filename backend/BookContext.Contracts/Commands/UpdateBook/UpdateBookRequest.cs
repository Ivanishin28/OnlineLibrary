using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.UpdateBook
{
    public class UpdateBookRequest : IResultRequest
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }
        [JsonPropertyName("title")]
        public string Title { get; private set; }
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; private set; }

        public UpdateBookRequest(Guid bookId, string title, ICollection<Guid> authorIds)
        {
            BookId = bookId;
            Title = title;
            AuthorIds = authorIds;
        }
    }
}
