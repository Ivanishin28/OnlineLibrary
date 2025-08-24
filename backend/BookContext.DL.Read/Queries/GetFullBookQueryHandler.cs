using BookContext.Contract.Dtos;
using BookContext.Contract.Queries.GetBook;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class GetFullBookQueryHandler : IRequestHandler<GetFullBookQuery, FullBookDto?>
    {
        private BookReadDbContext _db;

        public GetFullBookQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<FullBookDto?> Handle(GetFullBookQuery request, CancellationToken cancellationToken)
        {
            var fromDb = await _db.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .Where(x => x.Id == request.BookId)
                .FirstOrDefaultAsync();

            if (fromDb is null)
            {
                return null;
            }

            return new FullBookDto(
                fromDb.Id,
                fromDb.Title,
                fromDb.BookAuthors
                .Select(ba =>
                    new AuthorDto(
                        ba.Author.Id,
                        ba.Author.LastName,
                        ba.Author.FirstName,
                        ba.Author.MiddleName,
                        ba.Author.BirthDate))
                .ToList());
        }
    }
}
