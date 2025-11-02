using BookContext.Contract.Consts;
using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class SearchBookQueryHandler : IRequestHandler<SearchBookQuery, ICollection<BookPreviewDto>>
    {
        private BookReadDbContext _db;

        public SearchBookQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<BookPreviewDto>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Query) ||
                request.Query.Length <= BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH)
            {
                return new List<BookPreviewDto>();
            }

            var searchWords = request.Query
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLowerInvariant())
                .ToArray();

            if (searchWords.Length == 0)
                return new List<BookPreviewDto>();

            var query = _db.Books
                .Include(b => b.BookMetadata)
                .AsQueryable();

            foreach (var word in searchWords)
            {
                var searchTerm = $"%{word}%";
                query = query.Where(b => EF.Functions.Like((b.Title.ToLower()), searchTerm));
            }

            return await query
                .OrderBy(b => b.Title)
                .Take(BookQueryConsts.MAX_SEARCH_RESULTS)
                .Select(b => new BookPreviewDto(b.Id, b.Title, b.BookMetadata != null ? b.BookMetadata.CoverId : (Guid?)null))
                .ToListAsync(cancellationToken);
        }
    }
}

