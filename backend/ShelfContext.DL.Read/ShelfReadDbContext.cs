using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.Read.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read
{
    public class ShelfReadDbContext : DbContext
    {
        public DbSet<BookTagReadModel> BookTags { get; set; }
        public DbSet<ShelfReadModel> Shelves { get; set; }
        public DbSet<ShelvedBookReadModel> ShelvedBooks { get; set; }
        public DbSet<TagReadModel> Tags { get; set; }
        public DbSet<ReviewReadModel> Reviews { get; set; }

        public ShelfReadDbContext(DbContextOptions<ShelfReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookTagReadModel>().ToView("BookTags");
            modelBuilder.Entity<ShelfReadModel>().ToView("Shelves");
            modelBuilder.Entity<ShelvedBookReadModel>().ToView("ShelvedBooks");
            modelBuilder.Entity<TagReadModel>().ToView("Tags");
            modelBuilder.Entity<ReviewReadModel>().ToView("Reviews");
        }
    }
}
