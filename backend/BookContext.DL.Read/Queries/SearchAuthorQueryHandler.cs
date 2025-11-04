using BookContext.Contract.Consts;
using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class SearchAuthorQueryHandler : IRequestHandler<SearchAuthorQuery, ICollection<AuthorDto>>
    {
        private BookReadDbContext _db;

        public SearchAuthorQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<AuthorDto>> Handle(SearchAuthorQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Query) ||
                request.Query.Length < BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH)
            {
                return new List<AuthorDto>();
            }

            var searchWords = request.Query
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLowerInvariant())
                .ToArray();

            if (searchWords.Length == 0)
                return new List<AuthorDto>();

            var query = _db.Authors.AsQueryable();
            
            foreach (var word in searchWords)
            {
                var searchTerm = $"%{word}%";
                query = query.Where(a =>
                    EF.Functions.Like(a.FirstName.ToLower(), searchTerm) ||
                    EF.Functions.Like(a.LastName.ToLower(), searchTerm) ||
                    (a.MiddleName != null && EF.Functions.Like(a.MiddleName.ToLower(), searchTerm))
                );
            }

            return await query
                .Include(a => a.AuthorMetadata)
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Take(BookQueryConsts.MAX_SEARCH_RESULTS)
                .Select(a => new AuthorDto(
                    a.Id,
                    a.CreatorId,
                    a.FirstName,
                    a.LastName,
                    a.MiddleName,
                    a.AuthorMetadata.BirthDate,
                    a.AuthorMetadata != null ? a.AuthorMetadata.AvatarId : null,
                    a.AuthorMetadata != null ? a.AuthorMetadata.Biography : null))
                .ToListAsync(cancellationToken);
        }
    }
}

