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
    internal class TagReadModelEntityTypeConfiguration : IEntityTypeConfiguration<TagReadModel>
    {
        public void Configure(EntityTypeBuilder<TagReadModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .ToView("Tags");
        }
    }
}
