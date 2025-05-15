using ShelfContext.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.BooksOnShelves
{
    public record BookOnAShelfId(Guid Value) : EntityId<Guid>(Value);
}
