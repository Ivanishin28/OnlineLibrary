using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.Read.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.EntityTypeConfigurations
{
    internal class BookTagReadModelEntityTypeConfiguration : IEntityTypeConfiguration<BookTagReadModel>
    {
        public void Configure(EntityTypeBuilder<BookTagReadModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.ShelvedBook)
                .WithMany(e => e.BookTags)
                .HasForeignKey(e => e.ShelvedBookId);

            builder
                .HasOne(e => e.Tag)
                .WithMany(e => e.BookTags)
                .HasForeignKey(e => e.TagId);

            builder
                .ToView("BookTags");
        }
    }
}
