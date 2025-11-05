using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using BookContext.DL.SqlServer.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    internal class BookGenreEntityTypeConfiguration : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<BookGenreId, Guid>());

            builder
                .Property(x => x.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());
            builder
                .HasOne<Book>()
                .WithMany(x => x.BookGenres)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.GenreId)
                .HasConversion(new EntityIdValueConverter<GenreId, Guid>());
            builder
                .HasOne<Genre>()
                .WithMany()
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(x => new { x.BookId, x.GenreId })
                .IsUnique(true);
        }
    }
}

