using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetUserTagsQueryHandler : IRequestHandler<GetUserTagsQuery, ICollection<TagDto>>
    {
        private ShelfReadDbContext _db;

        public GetUserTagsQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<TagDto>> Handle(GetUserTagsQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Tags
                .Where(x => x.UserId == request.UserId)
                .Select(x => new TagDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId
                })
                .ToListAsync();
        }
    }
}
