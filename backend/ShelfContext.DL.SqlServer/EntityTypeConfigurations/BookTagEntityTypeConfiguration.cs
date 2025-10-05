using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.BookTags;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookTagEntityTypeConfiguration : IEntityTypeConfiguration<BookTag>
    {
        public void Configure(EntityTypeBuilder<BookTag> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasConversion(new EntityIdValueConverter<BookTagId, Guid>());

            builder
                .Property(e => e.ShelvedBookId)
                .HasConversion(new EntityIdValueConverter<ShelvedBookId, Guid>());

            builder
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(e => e.TagId);

            builder
                .Property(e => e.TagId)
                .HasConversion(new EntityIdValueConverter<TagId, Guid>());
        }
    }
}
