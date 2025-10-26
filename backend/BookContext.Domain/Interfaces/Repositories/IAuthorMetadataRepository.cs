using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IAuthorMetadataRepository
    {
        Task<AuthorMetadata?> GetBy(AuthorMetadataId id);
        Task<AuthorMetadata?> GetBy(AuthorId authorId);
        void Add(AuthorMetadata metadata);
        void Remove(AuthorMetadata metadata);
    }
}
