using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Queries.GetLibrarySummary;

namespace ShelfContext.DL.Read.Queries
{
    public class GetLibrarySummaryForUserQueryHandler : IRequestHandler<GetLibrarySummaryForUserQuery, LibrarySummary>
    {
        private readonly ShelfReadDbContext _db;

        public GetLibrarySummaryForUserQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<LibrarySummary> Handle(GetLibrarySummaryForUserQuery request, CancellationToken cancellationToken)
        {
            var shelves = await _db.Shelves
                .Where(s => s.UserId == request.UserId)
                .Select(s => new ShelfSummary(
                    s.Id,
                    s.Name,
                    s.ShelvedBooks.Count))
                .ToListAsync(cancellationToken);

            var tags = await _db.Tags
                .Where(t => t.UserId == request.UserId)
                .Select(t => new TagSummary(
                    t.Id,
                    t.Name,
                    t.BookTags.Count()))
                .ToListAsync(cancellationToken);

            return new LibrarySummary(
                request.UserId,
                shelves.Sum(x => x.BookCount),
                shelves,
                tags);
        }
    }
}
