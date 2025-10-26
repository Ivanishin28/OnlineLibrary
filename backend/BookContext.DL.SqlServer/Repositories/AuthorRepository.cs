using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces.Repositories;
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

        public async Task<Author?> GetBy(Guid id)
        {
            return await _dbSet
                .Where(author => author.Id.Value == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetByIds(IEnumerable<Guid> ids)
        {
            return _dbSet
                .Where(author => 
                    ids.Contains(author.Id.Value));
        }
    }
}
