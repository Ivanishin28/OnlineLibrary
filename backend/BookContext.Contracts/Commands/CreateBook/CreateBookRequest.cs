using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateBook
{
    public class CreateBookRequest : IResultRequest<CreateBookResponse>
    {
        [JsonPropertyName("title")]
        public string Title { get; private set; }
        [JsonPropertyName("author_ids")]
        public ICollection<Guid> AuthorIds { get; private set; }

        public CreateBookRequest(string title, ICollection<Guid> authorIds)
        {
            Title = title;
            AuthorIds = authorIds;
        }
    }
}
