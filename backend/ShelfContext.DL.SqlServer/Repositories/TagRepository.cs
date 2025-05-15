using Microsoft.EntityFrameworkCore;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.Repositories
{
    public class TagRepository : ITagRepository
    {
        private DbSet<Tag> _dbSet;

        public TagRepository(ShelfDbContext db)
        {
            _dbSet = db.Tags;
        }

        public async Task Add(Tag tag)
        {
            _dbSet.Add(tag);
        }

        public async Task<Tag> GetBy(TagId id)
        {
            return await _dbSet
                .Where(tag => tag.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Remove(Tag tag)
        {
            _dbSet.Remove(tag);
        }
    }
}
