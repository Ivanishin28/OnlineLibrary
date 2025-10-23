using Shared.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Commands.CreateAuthor
{
    public record CreateAuthorRequest(Guid CreatorId, string FirstName, string LastName, DateOnly BirthDate) : IResultRequest<Guid?>
    {
    }
}
