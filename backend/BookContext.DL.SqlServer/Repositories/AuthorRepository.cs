using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.SqlServer.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private DbSet<Author> _dbSet;

        public AuthorRepository(BookDbContext db)
        {
            _dbSet = db.Authors;
        }

        public void Add(Author author)
        {
            _dbSet.Add(author);
        }

        public void Remove(Author author)
        {
            _dbSet.Remove(author);
        }

        public async Task<Author?> GetBy(AuthorId id)
        {
            return await _dbSet
                .Where(author => author.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<AuthorId>> EnsureExist(ICollection<AuthorId> authorIds)
        {
            return await _dbSet
                .Where(x => authorIds.Any(id => x.Id == id))
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
