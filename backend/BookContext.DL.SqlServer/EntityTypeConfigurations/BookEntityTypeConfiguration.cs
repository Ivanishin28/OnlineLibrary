using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());

            builder
                .Property(x => x.CreatorId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());
        }
    }
}
