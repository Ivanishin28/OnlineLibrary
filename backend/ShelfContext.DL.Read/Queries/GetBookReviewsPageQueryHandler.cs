using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using ShelfContext.DL.Read.Entities;
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

        public async Task<Pagination<ReviewDto>> Handle(GetBookReviewsPageQuery request, CancellationToken cancellationToken)
        {
            var reviews = ApplyFilter(request.BookId);

            var total = await reviews
                .CountAsync();

            var items = await reviews
                .Where(x => x.BookId == request.BookId)
                .OrderByDescending(x => x.UpdatedAt)
                .Skip(request.Page.PageIndex * request.Page.PageSize)
                .Take(request.Page.PageSize)
                .Select(x => new ReviewDto()
                {
                    Id = x.Id,
                    BookId = x.BookId,
                    UserId = x.UserId,
                    Rating = x.Rating,
                    Text = x.Text
                })
                .ToListAsync();

            return new Pagination<ReviewDto>(total, items);
        }

        private IQueryable<ReviewReadModel> ApplyFilter(Guid bookId)
        {
            return _db
                .Reviews
                .Where(x => x.BookId == bookId);
        }
    }
}
