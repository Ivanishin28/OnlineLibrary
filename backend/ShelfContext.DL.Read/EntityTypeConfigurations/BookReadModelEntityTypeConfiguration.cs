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
    internal class BookReadModelEntityTypeConfiguration : IEntityTypeConfiguration<BookReadModel>
    {
        public void Configure(EntityTypeBuilder<BookReadModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .ToView("Books");
        }
    }
}
