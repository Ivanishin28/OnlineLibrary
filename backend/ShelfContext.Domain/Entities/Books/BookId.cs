using Shared.Core.ValueObjects;
using ShelfContext.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Books
{
    public record BookId(Guid Value) : EntityId<Guid>(Value);
}
