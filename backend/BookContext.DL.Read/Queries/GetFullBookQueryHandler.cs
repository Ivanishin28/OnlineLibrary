using BookContext.Contract.Dtos;
using BookContext.Contract.Queries.GetFullBook;
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
                .Include(x => x.BookMetadata)
                .Where(x => x.Id == request.BookId)
                .FirstOrDefaultAsync();

            if (fromDb is null)
            {
                return null;
            }

            return new FullBookDto
            {
                Id = fromDb.Id,
                Title = fromDb.Title,
                CoverId = fromDb.BookMetadata.CoverId,
                PublishingDate = fromDb.BookMetadata.PublishingDate,
                Description = fromDb.BookMetadata.Description,
                Authors = fromDb.BookAuthors
                    .Select(ba =>
                        new AuthorDto(
                            ba.Author.Id,
                            ba.Author.FirstName,
                            ba.Author.LastName,
                            ba.Author.MiddleName,
                            ba.Author.BirthDate))
                    .ToList()
            };
        }
    }
}
