using Composition.Contract.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using ShelfContext.DL.Read.Entities;

namespace ShelfContext.DL.Read.Queries
{
    public class GetLibraryPageQueryHandler : IRequestHandler<GetLibraryPageQuery, Pagination<ShelvedBookDto>>
    {
        private ShelfReadDbContext _db;

        public GetLibraryPageQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<Pagination<ShelvedBookDto>> Handle(GetLibraryPageQuery request, CancellationToken cancellationToken)
        {
            var shelvedBooks = _db
                .ShelvedBooks
                .Where(x => x.UserId == request.UserId);

            shelvedBooks = ApplyFilter(shelvedBooks, request.Filter);

            var total = await shelvedBooks
                .CountAsync();

            var items = await shelvedBooks
                .OrderBy(x => x.DateShelved)
                .Skip(request.Page.PageIndex * request.Page.PageSize)
                .Take(request.Page.PageSize)
                .Select(x => new ShelvedBookDto(
                    x.Id,
                    x.BookId,
                    x.ShelfId,
                    x.DateShelved,
                    x.BookTags.Select(x => new BookTagDto()
                    {
                        DateAdded = x.DateAdded,
                        Id = x.Id,
                        Name = x.Tag.Name,
                        ShelvedBookId = x.ShelvedBookId,
                        TagId = x.TagId
                    })
                    .ToList()))
                .ToListAsync();

            return new Pagination<ShelvedBookDto>(total, items);
        }

        public static IQueryable<ShelvedBookReadModel> ApplyFilter(
            IQueryable<ShelvedBookReadModel> shelvedBooks, 
            LibraryFilter filter)
        {
            if (filter.ShelfId is null && filter.TagId is null)
            {
                return shelvedBooks;
            }
            else if (filter.ShelfId is not null)
            {
                return shelvedBooks
                    .Where(x => x.ShelfId == filter.ShelfId);
            }
            else
            {
                return shelvedBooks
                    .Where(x => x.BookTags.Any(tag => tag.TagId == filter.TagId));
            }
        }
    }
}
