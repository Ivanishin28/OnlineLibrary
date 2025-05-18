using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class ShelfEntityTypeConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasConversion(new EntityIdValueConverter<ShelfId, Guid>());

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId);

            builder
                .Property(e => e.UserId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());

            builder
                .OwnsOne(
                e => e.Name,
                owned =>
                {
                    owned
                        .Property(oe => oe.Value)
                        .HasMaxLength(TagName.MAX_LENGTH)
                        .HasColumnName("Shelf_Name");
                }
            );
        }
    }
}
