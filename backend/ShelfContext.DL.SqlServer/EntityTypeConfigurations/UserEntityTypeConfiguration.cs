using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());

            builder
                .ToView("Users");
        }
    }
}
