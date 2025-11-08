using System;

namespace BookContext.Domain.ValueObjects
{
    public record BookGenreId(Guid Value) : EntityId<Guid>(Value)
    {
    }
}

