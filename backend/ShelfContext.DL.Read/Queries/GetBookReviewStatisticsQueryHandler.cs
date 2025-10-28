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
            var avg = await _db
                .Reviews
                .Where(x => x.BookId == request.BookId)
                .AverageAsync(x => x.Rating);

            return new BookReviewStatistics()
            {
                AvgRating = avg,
                ReviewCount = count
            };
        }
    }
}
