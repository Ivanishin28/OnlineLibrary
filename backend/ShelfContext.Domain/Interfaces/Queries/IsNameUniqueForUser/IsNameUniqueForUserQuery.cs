using MediatR;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.Domain.Interfaces.Queries.IsNameUniqueForUser
{
    public class IsNameUniqueForUserQuery : IRequest<bool>
    {
        public ShelfName ShelfName { get; private set; }
        public UserId UserId { get; private set; }

        public IsNameUniqueForUserQuery(ShelfName shelfName, UserId userId)
        {
            ShelfName = shelfName;
            UserId = userId;
        }
    }
}
