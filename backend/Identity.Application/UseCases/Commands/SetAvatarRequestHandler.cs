using IdentityContext.Contracts.Commands;
using IdentityContext.DL;
using IdentityContext.DL.Entities.ApplicationUser;
using IdentityContext.Integration.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityContext.Application.UseCases.Commands
{
    public class SetAvatarRequestHandler : IRequestHandler<SetAvatarRequest>
    {
        private ApplicationIdentityDbContext _db;
        private IBus _bus;

        public SetAvatarRequestHandler(ApplicationIdentityDbContext db, IBus bus)
        {
            _db = db;
            _bus = bus;
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

            if (user.AvatarId == request.AvatarId)
            {
                return;
            }

            var oldAvatar = user.AvatarId;
            user.AvatarId = request.AvatarId;

            await PublishEvents(user, oldAvatar);

            await _db.SaveChangesAsync();
        }

        private async Task PublishEvents(ApplicationUser user, Guid? oldAvatar)
        {
            if (oldAvatar.HasValue)
            {
                await _bus.Publish(
                    new UserAvatarRemovedIntegrationEvent(user.Id, oldAvatar.Value));
            }
            if (user.AvatarId.HasValue)
            {
                await _bus.Publish(
                    new UserAvatarSetIntegrationEvent(user.Id, user.AvatarId.Value));
            }
        }
    }
}
