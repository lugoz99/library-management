using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(item => item.BookTitle).HasMaxLength(300).IsRequired();
        builder.Property(item => item.UnitPrice).HasPrecision(18, 2);
        builder.HasOne(item => item.Book)
            .WithMany()
            .HasForeignKey(item => item.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(item => item.BookFormat)
            .WithMany()
            .HasForeignKey(item => item.BookFormatId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
