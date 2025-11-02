using IdentityContext.Contracts.Dtos;
using IdentityContext.Contracts.Queries;
using IdentityContext.DL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityContext.Application.UseCases.Queries
{
    public class GetIdentityPreviewByUserIdQueryHandler 
        : IRequestHandler<GetIdentityPreviewByUserIdQuery, IdentityPreviewDto?>
    {
        private ApplicationIdentityDbContext _db;

        public GetIdentityPreviewByUserIdQueryHandler(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public async Task<IdentityPreviewDto?> Handle(
            GetIdentityPreviewByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _db
                .Users
                .Where(x => x.UserId != null && x.UserId.Value == request.UserId)
                .Select(x => new IdentityPreviewDto()
                {
                    UserId = x.UserId!.Value,
                    IdentityId = x.Id,
                    Nickname = x.UserName!,
                    AvatarId = x.AvatarId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}

