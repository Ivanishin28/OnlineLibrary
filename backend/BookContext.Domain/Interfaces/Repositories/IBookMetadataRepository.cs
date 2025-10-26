using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IBookMetadataRepository
    {
        Task<BookMetadata?> GetBy(BookMetadataId id);
        Task<BookMetadata?> GetBy(BookId bookId);
        void Add(BookMetadata metadata);
        void Remove(BookMetadata metadata);
    }
}
