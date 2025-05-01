using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}
