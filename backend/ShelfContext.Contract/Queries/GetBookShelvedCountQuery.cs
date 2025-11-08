using MediatR;
using ShelfContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries
{
    public record GetBookShelvedCountQuery(Guid BookId) : IRequest<int>
    {
    }
}
