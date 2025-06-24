using MediatR;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser
{
    public class IsShelfNameTakenByUserQuery : IRequest<bool>
    {
        public ShelfName ShelfName { get; private set; }
        public UserId UserId { get; private set; }

        public IsShelfNameTakenByUserQuery(ShelfName shelfName, UserId userId)
        {
            ShelfName = shelfName;
            UserId = userId;
        }
    }
}
