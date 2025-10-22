using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using BookContext.DL.SqlServer.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookAuthorEntityTypeConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());

            builder
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Author>()
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
