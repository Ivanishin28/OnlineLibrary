using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries.GetShelvedBookByBookId;

namespace ShelfContext.DL.Read.Queries
{
    public class GetShelvedBookByBookIdRequestHandler : IRequestHandler<GetShelvedBookByBookIdRequest, ShelvedBookDto?>
    {
        private ShelfReadDbContext _db;

        public GetShelvedBookByBookIdRequestHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<ShelvedBookDto?> Handle(GetShelvedBookByBookIdRequest request, CancellationToken cancellationToken)
        {
            return await _db
                .ShelvedBooks
                .Where(x =>
                    x.BookId == request.BookId &&
                    x.Shelf.UserId == request.UserId)
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
                .FirstOrDefaultAsync();
        }
    }
}
