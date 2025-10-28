using IdentityContext.Contracts.Dtos;
using IdentityContext.Contracts.Queries;
using IdentityContext.DL;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityContext.Application.UseCases.Queries
{
    public class GetIdentityPreviewsFromUserIdsQueryHandler 
        : IRequestHandler<GetIdentityPreviewsFromUserIdsQuery, ICollection<IdentityPreviewDto>>
    {
        private ApplicationIdentityDbContext _db;

        public GetIdentityPreviewsFromUserIdsQueryHandler(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<IdentityPreviewDto>> Handle(
            GetIdentityPreviewsFromUserIdsQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Users
                .Where(x =>
                    x.UserId != null &&
                    request.UserIds.Contains(x.UserId.Value))
                .Select(x => new IdentityPreviewDto()
                {
                    UserId = x.UserId!.Value,
                    IdentityId = x.Id,
                    Nickname = x.UserName!,
                })
                .ToListAsync();
        }
    }
}
