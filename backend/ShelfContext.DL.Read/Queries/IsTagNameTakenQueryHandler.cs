using MediatR;
using ShelfContext.Contract.Queries;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Queries
{
    public class IsTagNameTakenQueryHandler : IRequestHandler<IsTagNameTakenQuery, bool>
    {
        private ITagNameUniquenessChecker _checker;

        public IsTagNameTakenQueryHandler(ITagNameUniquenessChecker checker)
        {
            _checker = checker;
        }

        public async Task<bool> Handle(IsTagNameTakenQuery request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            var tagNameResult = TagName.Create(request.TagName);
            
            if (tagNameResult.IsFailure)
            {
                return false;
            }

            return await _checker.IsNameTakenBy(tagNameResult.Model, userId);
        }
    }
}
