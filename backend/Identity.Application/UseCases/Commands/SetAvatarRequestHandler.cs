using IdentityContext.Contracts.Commands;
using IdentityContext.Contracts.Dtos;
using IdentityContext.DL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.UseCases.Commands
{
    public class SetAvatarRequestHandler : IRequestHandler<SetAvatarRequest>
    {
        private ApplicationIdentityDbContext _db;

        public SetAvatarRequestHandler(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public async Task Handle(SetAvatarRequest request, CancellationToken cancellationToken)
        {
            var user = await _db
                .Users
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync();
            if (user is null)
            {
                return;
            }

            user.AvatarId = request.AvatarId;
            await _db.SaveChangesAsync();
        }
    }
}
