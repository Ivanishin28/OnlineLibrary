using MediaContext.Application.Contracts.Queries;
using MediaContext.Application.Dtos;
using MediaContext.DL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaContext.Application.UseCases.Queries
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, MediaFileDto?>
    {
        private MediaDbContext _db;

        public GetFileQueryHandler(MediaDbContext db)
        {
            _db = db;
        }

        public async Task<MediaFileDto?> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _db
                .MediaFiles
                .FirstOrDefaultAsync(x => 
                    x.Id == request.FileId);
            if (file is null)
            {
                return null;
            }

            var dto = new MediaFileDto()
            {
                Id = file.Id,
                Content = file.Content
            };
            return dto;
        }
    }
}
