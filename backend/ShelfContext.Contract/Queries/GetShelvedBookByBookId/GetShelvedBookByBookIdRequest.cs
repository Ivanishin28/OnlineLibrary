using MediatR;
using ShelfContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Queries.GetShelvedBookByBookId
{
    public record GetShelvedBookByBookIdRequest(Guid UserId, Guid BookId) : IRequest<ShelvedBookDto?>
    {
    }
}
