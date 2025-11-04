using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class GetFullAuthorQueryHandler : IRequestHandler<GetFullAuthorQuery, AuthorDto?>
    {
        private BookReadDbContext _db;

        public GetFullAuthorQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<AuthorDto?> Handle(GetFullAuthorQuery request, CancellationToken cancellationToken)
        {
            var fromDb = await _db.Authors
                .Include(x => x.AuthorMetadata)
                .Where(x => x.Id == request.AuthorId)
                .FirstOrDefaultAsync(cancellationToken);

            if (fromDb is null)
            {
                return null;
            }

            return new AuthorDto(
                fromDb.Id,
                fromDb.CreatorId,
                fromDb.FirstName,
                fromDb.LastName,
                fromDb.MiddleName,
                fromDb.AuthorMetadata != null ? fromDb.AuthorMetadata.BirthDate : default(DateOnly),
                fromDb.AuthorMetadata != null ? fromDb.AuthorMetadata.AvatarId : null,
                fromDb.AuthorMetadata != null ? fromDb.AuthorMetadata.Biography : null);
        }
    }
}
