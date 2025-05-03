using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateBook
{
    public class CreateBookResponse
    {
        [JsonPropertyName("book_id")]
        public Guid BookId { get; private set; }

        public CreateBookResponse(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
