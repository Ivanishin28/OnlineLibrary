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
    public class AuthorMetadataRepository : IAuthorMetadataRepository
    {
        private BookDbContext _db;

        public AuthorMetadataRepository(BookDbContext db)
        {
            _db = db;
        }

        public void Add(AuthorMetadata metadata)
        {
            _db.Add(metadata);
        }

        public Task<AuthorMetadata?> GetBy(AuthorMetadataId id)
        {
            return _db
                .AuthorMetadatas
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<AuthorMetadata?> GetBy(AuthorId authorId)
        {
            return _db
                .AuthorMetadatas
                .Where(x => x.AuthorId == authorId)
                .FirstOrDefaultAsync();
        }

        public void Remove(AuthorMetadata metadata)
        {
            _db.AuthorMetadatas.Remove(metadata);
        }
    }
}
