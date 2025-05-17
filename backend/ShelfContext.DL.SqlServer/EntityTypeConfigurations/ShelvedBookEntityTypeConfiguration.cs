using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.Domain.Entities.ShelvedBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class ShelvedBookEntityTypeConfiguration : IEntityTypeConfiguration<ShelvedBook>
    {
        public void Configure(EntityTypeBuilder<ShelvedBook> builder)
        {
        }
    }
}
