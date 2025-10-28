using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetBookReviewsPageQueryHandler : IRequestHandler<GetBookReviewsPageQuery, Pagination<ReviewDto>>
    {
        private ShelfReadDbContext _db;

        public GetBookReviewsPageQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public Task<Pagination<ReviewDto>> Handle(GetBookReviewsPageQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
