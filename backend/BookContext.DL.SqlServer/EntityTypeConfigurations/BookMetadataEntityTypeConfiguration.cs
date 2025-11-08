using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    internal class BookMetadataEntityTypeConfiguration : IEntityTypeConfiguration<BookMetadata>
    {
        public void Configure(EntityTypeBuilder<BookMetadata> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<BookMetadataId, Guid>());

            builder
                .HasOne<Book>()
                .WithOne()
                .HasForeignKey<BookMetadata>(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Property(x => x.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());

            builder
                .Property(x => x.PublishingDate)
                .IsRequired();

            builder
                .Property(x => x.CoverId)
                .HasConversion(
                    id => id == null ? (Guid?)null : id.Value,
                    value => value == null ? null : new MediaFileId(value.Value)
                )
                .IsRequired(false);
            builder
                .Property(x => x.FileId)
                .HasConversion(
                    id => id == null ? (Guid?)null : id.Value,
                    value => value == null ? null : new MediaFileId(value.Value)
                )
                .IsRequired(false);

            builder
                .OwnsOne(
                    x => x.Description,
                    owned =>
                    {
                        owned
                            .Property(o => o.Value)
                            .HasMaxLength(BookDescription.MAX_LENGTH)
                            .IsRequired(false)
                            .HasColumnName("Description");
                    });
        }
    }
}
