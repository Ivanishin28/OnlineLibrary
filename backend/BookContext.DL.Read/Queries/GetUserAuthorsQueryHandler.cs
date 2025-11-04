using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.Read.Queries
{
    public class GetUserAuthorsQueryHandler : IRequestHandler<GetUserAuthorsQuery, ICollection<AuthorPreview>>
    {
        private BookReadDbContext _db;

        public GetUserAuthorsQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<AuthorPreview>> Handle(GetUserAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Authors
                .Include(x => x.AuthorMetadata)
                .Where(x => x.CreatorId == request.UserId)
                .Select(x => new AuthorPreview()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    AvatarId = x.AuthorMetadata != null ? x.AuthorMetadata.AvatarId : null
                })
                .ToListAsync();
        }
    }
}
