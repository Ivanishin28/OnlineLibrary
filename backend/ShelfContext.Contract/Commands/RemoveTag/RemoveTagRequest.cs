using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Commands.RemoveTag
{
    public record RemoveTagRequest(Guid ShelvedBookId, Guid TagId, Guid UserId) : IResultRequest
    {
    }
}
