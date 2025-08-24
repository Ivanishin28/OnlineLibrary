using BookContext.Contract.Dtos;
using BookContext.Contract.Queries.GetAllBooks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookPreviewDto>>
    {
        private BookReadDbContext _db;

        public GetAllBooksQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BookPreviewDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books
                .Select(x => new BookPreviewDto(x.Id, x.Title))
                .ToListAsync();
        }
    }
}
