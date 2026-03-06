using BookContext.DL.SqlServer.EntityTypeConfigurations;
using BookContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BookContext.Domain.Interfaces;

namespace BookContext.DL.SqlServer
{
    public class BookDbContext : DbContext
    {
        private IPublisher _publisher;

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorMetadata> AuthorMetadatas { get; set; }
        public DbSet<BookMetadata> BookMetadatas { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var currentAssembly = typeof(BookEntityTypeConfiguration).Assembly;
            builder.ApplyConfigurationsFromAssembly(currentAssembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .SelectMany(x => x.DomainEvents);

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
