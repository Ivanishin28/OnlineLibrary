using MediatR;
using ShelfContext.DL.Read.Queries.IsNameUniqueForUser;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Queries
{
    public class ShelfNameUniquenessChecker : IShelfNameUniquenessChecker
    {
        private IsShelfNameTakenByUserQueryHandler _handler;

        public ShelfNameUniquenessChecker(IsShelfNameTakenByUserQueryHandler handler)
        {
            _handler = handler;
        }

        public async Task<bool> IsNameTakenBy(ShelfName name, UserId userId)
        {
            var query = new IsShelfNameTakenByUserQuery(name, userId);
            return await _handler.Handle(query, CancellationToken.None);
        }
    }
}
