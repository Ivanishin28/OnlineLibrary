using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read.Queries
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
    {
        private BookReadDbContext _db;

        public GetAllGenresQueryHandler(BookReadDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}

