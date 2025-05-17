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
        [JsonPropertyName("shelved_book_id")]
        public Guid ShelvedBookId { get; private set; }

        public DislodgeBookRequest(Guid shelvedBookId)
        {
            ShelvedBookId = shelvedBookId;
        }
    }
}
