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

        public async Task Add(Author author)
        {
            _dbSet.Add(author);
        }

        public async Task Delete(Author author)
        {
            _dbSet.Remove(author);
        }

        public async Task<Author?> GetBy(Guid id)
        {
            return await _dbSet
                .Where(author => author.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetByIds(IEnumerable<Guid> ids)
        {
            return _dbSet
                .Where(author => 
                    ids.Contains(author.Id));
        }
    }
}
