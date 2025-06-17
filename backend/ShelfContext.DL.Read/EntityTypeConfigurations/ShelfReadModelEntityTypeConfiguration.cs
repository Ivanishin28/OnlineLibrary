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
    internal class ShelfReadModelEntityTypeConfiguration : IEntityTypeConfiguration<ShelfReadModel>
    {
        public void Configure(EntityTypeBuilder<ShelfReadModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.User)
                .WithMany(e => e.Shelves)
                .HasForeignKey(e => e.UserId);

            builder
                .ToView("Shelves");
        }
    }
}
