using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Constants;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<AuthorId, Guid>());

            builder
                .Property(x => x.CreatorId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());

            builder.OwnsOne(
                a => a.FullName,
                fullName =>
                {
                    fullName
                        .Property(f => f.FirstName)
                        .HasConversion<NameComponentValueConverter>()
                        .HasMaxLength(AuthorConstants.MAX_NAME_COMPONENT_LENGTH)
                        .IsRequired()
                        .HasColumnName("FirstName");

                    fullName
                        .Property(f => f.LastName)
                        .HasConversion<NameComponentValueConverter>()
                        .HasMaxLength(AuthorConstants.MAX_NAME_COMPONENT_LENGTH)
                        .IsRequired()
                        .HasColumnName("LastName");


                    fullName
                        .Property(f => f.MiddleName)
                        .HasConversion<NameComponentValueConverter>()
                        .HasMaxLength(AuthorConstants.MAX_NAME_COMPONENT_LENGTH)
                        .IsRequired(false)
                        .HasColumnName("MiddleName");
                }
            );
        }
    }
}
