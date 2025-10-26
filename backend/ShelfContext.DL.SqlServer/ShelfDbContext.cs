using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer.EntityTypeConfigurations;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer
{
    public class ShelfDbContext : DbContext
    {
        public DbSet<Book> Books { get; private set; }
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<Shelf> Shelves { get; private set; }
        public DbSet<ShelvedBook> ShelvedBooks { get; private set; }
        public DbSet<BookTag> BookTags { get; private set; }
        public DbSet<Review> Reviews { get; private set; }

        public ShelfDbContext(DbContextOptions<ShelfDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurationAssembly = typeof(ShelfEntityTypeConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);
        }
    }
}
