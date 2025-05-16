using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer.EntityTypeConfigurations;
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
        public DbSet<User> Users { get; private set; }
        public DbSet<Tag> Tags { get; private set; } 

        public ShelfDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
