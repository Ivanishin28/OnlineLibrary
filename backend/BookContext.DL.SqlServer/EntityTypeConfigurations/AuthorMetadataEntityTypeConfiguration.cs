using BookContext.DL.SqlServer.ValueConverters;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.DL.SqlServer.EntityTypeConfigurations
{
    internal class AuthorMetadataEntityTypeConfiguration : IEntityTypeConfiguration<AuthorMetadata>
    {
        public void Configure(EntityTypeBuilder<AuthorMetadata> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<AuthorMetadataId, Guid>());

            builder
                .HasOne<Author>()
                .WithOne()
                .HasForeignKey<AuthorMetadata>(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Property(x => x.AuthorId)
                .HasConversion(new EntityIdValueConverter<AuthorId, Guid>());

            builder
                .Property(x => x.AvatarId)
                .HasConversion(
                    id => id == null ? (Guid?)null : id.Value,
                    value => value == null ? null : new MediaFileId(value.Value)
                )
                .IsRequired(false);

            builder
                .OwnsOne(
                    x => x.Biography,
                    owned =>
                    {
                        owned
                            .Property(o => o.Value)
                            .HasMaxLength(AuthorBiography.MAX_LENGTH)
                            .HasColumnName("Biography");
                    });
        }
    }
}
