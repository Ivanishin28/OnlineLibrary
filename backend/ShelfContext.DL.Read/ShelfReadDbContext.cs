using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.Read.Entities;
using ShelfContext.DL.Read.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read
{
    public class ShelfReadDbContext : DbContext
    {
        public DbSet<BookReadModel> Books { get; set; }
        public DbSet<BookTagReadModel> BookTags { get; set; }
        public DbSet<ShelfReadModel> Shelves { get; set; }
        public DbSet<ShelvedBookReadModel> ShelvedBooks { get; set; }
        public DbSet<TagReadModel> Tags { get; set; }
        public DbSet<UserReadModel> Users { get; set; }


        public ShelfReadDbContext(DbContextOptions<ShelfReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurationAssembly = typeof(ShelfReadModelEntityTypeConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);
        }
    }
}
