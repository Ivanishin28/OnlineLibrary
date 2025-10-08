using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasConversion(new EntityIdValueConverter<TagId, Guid>());

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder
                .Property(e => e.UserId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());

            builder.OwnsOne(
                e => e.Name, 
                owned =>
                {
                    owned
                        .Property(oe => oe.Value)
                        .HasMaxLength(TagName.MAX_LENGTH)
                        .HasColumnName("Name");
                }
            );
        }
    }
}
