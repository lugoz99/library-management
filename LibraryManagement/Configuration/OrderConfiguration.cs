using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.TotalAmount).HasPrecision(18, 2);
        builder.Property(order => order.Currency).HasMaxLength(3).IsRequired();
        builder.Property(order => order.StripeCheckoutSessionId).HasMaxLength(255);
        builder.Property(order => order.StripePaymentIntentId).HasMaxLength(255);
        builder.HasIndex(order => order.StripeCheckoutSessionId).IsUnique()
            .HasFilter("[StripeCheckoutSessionId] IS NOT NULL");

        builder.HasOne(order => order.User)
            .WithMany()
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(order => order.Items)
            .WithOne(item => item.Order)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
