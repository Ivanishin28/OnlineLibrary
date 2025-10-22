using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.ValueObjects
{
    public record AuthorId(Guid Value) : EntityId<Guid>(Value) 
    {
    }
}