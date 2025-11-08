using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private BookDbContext _db;

        public GenreRepository(BookDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<GenreId>> EnsureExist(ICollection<GenreId> ids)
        {
            return await _db
                .Genres
                .Where(x => ids.Any(id => id == x.Id))
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<ICollection<Genre>> GetAll()
        {
            return await _db.Genres.ToListAsync();
        }
    }
}
