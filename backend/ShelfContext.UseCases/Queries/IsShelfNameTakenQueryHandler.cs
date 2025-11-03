using MediatR;
using ShelfContext.Contract.Queries;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Queries
{
    public class IsShelfNameTakenQueryHandler : IRequestHandler<IsShelfNameTakenQuery, bool>
    {
        private IShelfNameUniquenessChecker _checker;

        public IsShelfNameTakenQueryHandler(IShelfNameUniquenessChecker checker)
        {
            _checker = checker;
        }

        public async Task<bool> Handle(IsShelfNameTakenQuery request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            var shelfName = ShelfName.Create(request.Name);
            if (shelfName.IsFailure)
            {
                return false;
            }

            return await _checker.IsNameTakenBy(shelfName.Model, userId);
        }
    }
}
