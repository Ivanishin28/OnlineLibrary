using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetBookReviewStatisticsQueryHandler : IRequestHandler<GetBookReviewStatisticsQuery, BookReviewStatistics>
    {
        private ShelfReadDbContext _db;

        public GetBookReviewStatisticsQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<BookReviewStatistics> Handle(
            GetBookReviewStatisticsQuery request, 
            CancellationToken cancellationToken)
        {
            var count = await _db
                .Reviews
                .Where(x => x.BookId == request.BookId)
                .CountAsync();
            if (count == 0)
            {
                return new BookReviewStatistics()
                {
                    ReviewCount = 0,
                    AvgRating = 0
                };
            }
            var avg = await _db
                .Reviews
                .Where(x => x.BookId == request.BookId)
                .AverageAsync(x => x.Rating);
            return new BookReviewStatistics()
            {
                ReviewCount = count,
                AvgRating = avg,
            };
        }
    }
}
