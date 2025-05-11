using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.DislodgeBook
{
    public class DislodgeBookRequest : IResultRequest
    {
        [JsonPropertyName("book_on_a_shelf_id")]
        public Guid BookOnAShelfId { get; private set; }

        public DislodgeBookRequest(Guid bookOnAShelfId)
        {
            BookOnAShelfId = bookOnAShelfId;
        }
    }
}
