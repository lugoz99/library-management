using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.Property(promotion => promotion.Name)
            .IsRequired()
            .HasMaxLength(120);
        builder.Property(promotion => promotion.Description).HasMaxLength(500);
        builder.Property(promotion => promotion.DiscountValue).HasPrecision(18, 2);

        builder.HasOne(promotion => promotion.Book)
            .WithMany(book => book.Promotions)
            .HasForeignKey(promotion => promotion.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(promotion => new
        {
            promotion.BookId,
            promotion.StartsAt,
            promotion.EndsAt
        });
    }
}
