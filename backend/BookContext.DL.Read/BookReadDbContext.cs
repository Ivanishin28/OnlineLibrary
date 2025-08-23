﻿using BookContext.DL.Read.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookContext.DL.Read
{
    public class BookReadDbContext : DbContext
    {
        public DbSet<BookReadModel> Books { get; set; }
        public DbSet<AuthorReadModel> Authors { get; set; }
        public DbSet<BookAuthorReadModel> BookAuthors { get; set; }

        public BookReadDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<BookReadModel>()
                .ToView("Books");

            builder
                .Entity<AuthorReadModel>()
                .ToView("Authors");

            builder
                .Entity<BookAuthorReadModel>()
                .ToView("BookAuthors");
        }
    }
}
