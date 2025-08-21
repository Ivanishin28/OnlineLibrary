using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class ShelvedBookEntityTypeConfiguration : IEntityTypeConfiguration<ShelvedBook>
    {
        public void Configure(EntityTypeBuilder<ShelvedBook> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasConversion(new EntityIdValueConverter<ShelvedBookId, Guid>());

            builder
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(e => e.BookId);

            builder
                .Property(e => e.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());

            builder
                .HasOne<Shelf>()
                .WithMany()
                .HasForeignKey(e => e.ShelfId);

            builder
                .Property(e => e.ShelfId)
                .HasConversion(new EntityIdValueConverter<ShelfId, Guid>());

            const string bookTagsNavigation = "_bookTags";

            builder
                .Ignore(e => e.BookTags);

            builder
                .HasMany(typeof(BookTag), bookTagsNavigation)
                .WithOne()
                .HasForeignKey(nameof(BookTag.ShelvedBookId));

            builder
                .Metadata
                .FindNavigation(bookTagsNavigation)!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
