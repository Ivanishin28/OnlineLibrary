using BookContext.Contract.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Contract.Queries
{
    public record GetUserBooksQuery(Guid UserId) : IRequest<ICollection<BookPreviewDto>>
    {
    }
}
