using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DL.ValueConverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Domain.Entities;

namespace UserContext.DL.SqlServer.EntityTypeConfigurations
{
    public class UserAccountEntityTypeConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne<UserProfile>()
                .WithOne()
                .HasForeignKey<UserAccount>(e => e.ProfileId);

            builder
                .Property(e => e.Email)
                .HasConversion<EmailValueConverter>();
        }
    }
}
