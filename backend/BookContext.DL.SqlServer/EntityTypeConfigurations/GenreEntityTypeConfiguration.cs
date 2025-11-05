using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<GenreId, Guid>());
        }
    }
}
