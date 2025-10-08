using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class IsTagNameTakenQueryHandler : IRequestHandler<IsTagNameTakenQuery, bool>
    {
        private ShelfReadDbContext _db;

        public IsTagNameTakenQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<bool> Handle(IsTagNameTakenQuery request, CancellationToken cancellationToken)
        {
            return _db
                .Tags
                .AnyAsync(x =>
                    x.UserId == request.UserId &&
                    x.Name == request.TagName);
        }
    }
}
