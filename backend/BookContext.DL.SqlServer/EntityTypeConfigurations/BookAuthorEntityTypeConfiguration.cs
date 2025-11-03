using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using BookContext.DL.SqlServer.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookAuthorEntityTypeConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<BookAuthorId, Guid>());

            builder
                .Property(x => x.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());
            builder
                .HasOne<Book>()
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.AuthorId)
                .HasConversion(new EntityIdValueConverter<AuthorId, Guid>());
            builder
                .HasOne<Author>()
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(x => new { x.BookId, x.AuthorId })
                .IsUnique(true);
        }
    }
}
