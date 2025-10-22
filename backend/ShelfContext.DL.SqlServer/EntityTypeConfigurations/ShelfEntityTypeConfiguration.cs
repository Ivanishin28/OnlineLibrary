using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;

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
                .Property(e => e.UserId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());

            builder
                .OwnsOne(
                e => e.Name,
                p =>
                {
                    p
                        .Property(pe => pe.Value)
                        .HasMaxLength(ShelfName.MAX_LENGTH)
                        .HasColumnName("Name");
                }
            );
        }
    }
}
