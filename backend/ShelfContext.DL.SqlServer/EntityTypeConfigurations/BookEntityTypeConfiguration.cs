using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.Domain.Entities.Books;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.OwnsOne(e => e.Id, builder => builder.HasKey().HasName("Id"));

            builder.ToTable("Books");
        }
    }
}
