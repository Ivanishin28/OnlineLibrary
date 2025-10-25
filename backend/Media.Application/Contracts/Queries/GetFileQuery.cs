using MediaContext.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaContext.Application.Contracts.Queries
{
    public record GetFileQuery(Guid FileId) : IRequest<MediaFileDto?>
    {
    }
}
