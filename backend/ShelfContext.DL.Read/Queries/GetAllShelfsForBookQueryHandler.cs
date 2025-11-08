using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Queries
{
    public class GetAllShelfsForBookQueryHandler : IRequestHandler<GetAllShelfsForBookQuery, ICollection<ShelfForBook>>
    {
        private ShelfReadDbContext _db;

        public GetAllShelfsForBookQueryHandler(ShelfReadDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<ShelfForBook>> Handle(GetAllShelfsForBookQuery request, CancellationToken cancellationToken)
        {
            return await _db
                .Shelves
                .Where(shelf => shelf.ShelvedBooks.Any(sb => sb.BookId == request.BookId))
                .GroupBy(shelf => shelf.Name)
                .Select(nameGroup => new ShelfForBook()
                {
                    ShelfName = nameGroup.Key,
                    Count = nameGroup.Count()
                })
                .ToListAsync();
        }
    }
}
