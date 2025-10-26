using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.SqlServer.Repositories
{
    public class BookMetadataRepository : IBookMetadataRepository
    {
        private BookDbContext _db;

        public BookMetadataRepository(BookDbContext db)
        {
            _db = db;
        }

        public void Add(BookMetadata metadata)
        {
            _db.Add(metadata);
        }

        public Task<BookMetadata?> GetBy(BookMetadataId id)
        {
            return _db
                .BookMetadatas
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<BookMetadata?> GetBy(BookId bookId)
        {
            return _db
                .BookMetadatas
                .Where(x => x.BookId == bookId)
                .FirstOrDefaultAsync();
        }

        public void Remove(BookMetadata metadata)
        {
            _db.BookMetadatas.Remove(metadata);
        }
    }
}
