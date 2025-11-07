using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using BookContext.DL.Read.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Models;

namespace BookContext.DL.Read.Queries
{
    public class GetBookPageQueryHandler : IRequestHandler<GetBookPageQuery, Pagination<BookPreviewDto>>
    {
        private BookReadDbContext _db;

        public GetBookPageQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<Pagination<BookPreviewDto>> Handle(GetBookPageQuery request, CancellationToken cancellationToken)
        {
            var filtered = ApplyFilter(_db.Books, request.Filter);

            var count = await filtered
                .CountAsync();

            var books = await filtered
                .Skip(request.Page.PageSize * request.Page.PageIndex)
                .Take(request.Page.PageSize)
                .Select(x => new BookPreviewDto(x.Id, x.Title, x.BookMetadata.CoverId))
                .ToListAsync();

            var pagination = new Pagination<BookPreviewDto>(count, books);
            return pagination;
        }

        private IQueryable<BookReadModel> ApplyFilter(IQueryable<BookReadModel> books, BookFilter filter)
        {
            if (!filter.GenreIds.Any())
            {
                return books;
            }
            else
            {
                return books
                    .Where(book => book.BookGenres.All(bg => filter.GenreIds.Contains(bg.GenreId)));
            }
        }
    }
}
