using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfContext.DL.SqlServer.ValueConverters;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Users;

namespace ShelfContext.DL.SqlServer.EntityTypeConfigurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(new EntityIdValueConverter<ReviewId, Guid>());

            builder
                .OwnsOne(
                    x => x.Text,
                    owned =>
                    {
                        owned
                            .Property(o => o.Value)
                            .HasMaxLength(ReviewText.MAX_LENGTH)
                            .IsRequired(false)
                            .HasColumnName("Text");
                    });

            builder
                .OwnsOne(
                    x => x.Rating,
                    owned =>
                    {
                        owned
                            .Property(o => o.Value)
                            .HasColumnName("Rating");
                    });

            builder
                .Property(x => x.UserId)
                .HasConversion(new EntityIdValueConverter<UserId, Guid>());
            builder
                .Property(x => x.BookId)
                .HasConversion(new EntityIdValueConverter<BookId, Guid>());
        }
    }
}
