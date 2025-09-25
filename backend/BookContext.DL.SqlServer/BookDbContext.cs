using BookContext.DL.SqlServer.EntityTypeConfigurations;
using BookContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<User> Users { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var currentAssembly = typeof(BookEntityTypeConfiguration).Assembly;
            builder.ApplyConfigurationsFromAssembly(currentAssembly);
        }
    }
}
