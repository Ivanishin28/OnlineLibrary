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
    internal class ShelvedBookReadModelEntityTypeConfiguration : IEntityTypeConfiguration<ShelvedBookReadModel>
    {
        public void Configure(EntityTypeBuilder<ShelvedBookReadModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.Shelf)
                .WithMany(e => e.ShelvedBooks)
                .HasForeignKey(e => e.ShelfId);

            builder
                .ToView("ShelvedBooks");
        }
    }
}
